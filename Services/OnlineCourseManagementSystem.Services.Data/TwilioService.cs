namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.VideoConferences;
    using Twilio;
    using Twilio.Base;
    using Twilio.Jwt.AccessToken;
    using Twilio.Rest.Video.V1;
    using Twilio.Rest.Video.V1.Room;

    using MicrosoftOptions = Microsoft.Extensions.Options;
    using ParticipantStatus = Twilio.Rest.Video.V1.Room.ParticipantResource.StatusEnum;

    public class TwilioService : ITwilioService
    {
        private readonly TwilioSettings twilioSettings;

        public TwilioService(MicrosoftOptions.IOptions<TwilioSettings> twilioOptions)
        {
            this.twilioSettings = twilioOptions?.Value
                ?? throw new ArgumentNullException(nameof(twilioOptions));
            TwilioClient.Init(this.twilioSettings.ApiKey, this.twilioSettings.ApiSecret);
        }

        public TwilioJwt GetTwilioJwt(string identity)
        {
            return new TwilioJwt
            {
                Token = new Token(
                this.twilioSettings.AccountSid,
                this.twilioSettings.ApiKey,
                this.twilioSettings.ApiSecret,
                identity ?? Guid.NewGuid().ToString(),
                grants: new HashSet<IGrant> { new VideoGrant() })
                .ToJwt(),
            };
        }

        public IEnumerable<RoomDetails> GetAllRoomsAsync()
        {
            var rooms = RoomResource.Read();
            var tasks = rooms.Select(
                room => this.GetRoomDeatilsAsync(
                    room,
                    ParticipantResource.Read(
                        room.Sid,
                        ParticipantStatus.Connected)));
            return tasks.ToList();
        }

        private RoomDetails GetRoomDeatilsAsync(
            RoomResource room,
            ResourceSet<ParticipantResource> participantTask)
        {
            var participants = participantTask;
            return new RoomDetails
            {
                Id = room.UniqueName,
                MaxParticipants = room.MaxParticipants ?? 0,
                ParticipantCount = participants.Count(),
                Participants = participants.Select(p => new ParticipantViewModel()
                {
                    Id = p.Identity,
                    Name ="Test Name",
                }).ToList(),
            };
        }
    }
}
