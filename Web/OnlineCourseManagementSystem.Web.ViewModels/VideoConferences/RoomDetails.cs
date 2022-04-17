namespace OnlineCourseManagementSystem.Web.ViewModels.VideoConferences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class RoomDetails : IMapFrom<Room>
    {
        public RoomDetails()
        {
            this.Participants = new List<ParticipantViewModel>();
        }

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string CreatorId { get; set; }

        public int ParticipantCount { get; set; }

        public int MaxParticipants { get; set; }

        public bool IsDisplayCameraAllowed { get; set; }

        public bool IsUnmutedMicAllowed { get; set; }

        public bool IsShareScreenAllowed { get; set; }

        public List<ParticipantViewModel> Participants { get; set; }
    }
}
