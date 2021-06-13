using OnlineCourseManagementSystem.Data.Common.Repositories;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using OnlineCourseManagementSystem.Web.ViewModels.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public class ChannelsService : IChannelsService
    {
        private readonly IDeletableEntityRepository<Channel> channelsRepository;
        private readonly IDeletableEntityRepository<UserChannel> userChannelsRepository;

        public ChannelsService(IDeletableEntityRepository<Channel> channelsRepository, IDeletableEntityRepository<UserChannel> userChannelsRepository)
        {
            this.channelsRepository = channelsRepository;
            this.userChannelsRepository = userChannelsRepository;
        }

        public async Task CreateAsync(CreateChannelInputModel input)
        {
            Channel channel = new Channel
            {
                Code = input.Code,
                Name = input.Name,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CreatorId = input.CreatorId,
            };

            await this.channelsRepository.AddAsync(channel);
            await this.channelsRepository.SaveChangesAsync();

            UserChannel userChannel = new UserChannel
            {
                ChannelId = channel.Id,
                UserId = input.CreatorId,
            };

            await this.userChannelsRepository.AddAsync(userChannel);
            await this.userChannelsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByCreatorId<T>(string creatorId)
        {
            return this.channelsRepository
                .All()
                .Where(c => c.CreatorId == creatorId)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByParticipantId<T>(string participantId)
        {
            return this.channelsRepository
                .All()
                .Where(c => c.Users.Any(u => u.UserId == participantId))
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToList();
        }
    }
}
