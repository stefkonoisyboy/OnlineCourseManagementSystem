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
    using OnlineCourseManagementSystem.Web.ViewModels.Channels;

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
            if (this.channelsRepository.All().Any(c => c.Code == input.Code))
            {
                throw new InvalidOperationException("Channel with such a code already exists!");
            }

            if (input.StartDate <= DateTime.UtcNow)
            {
                throw new ArgumentException("Start Date should be greater than Current Date!");
            }

            if (input.StartDate >= input.EndDate)
            {
                throw new ArgumentException("End Date should be greater than Start Date!");
            }

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

        public async Task DeleteAsync(int id)
        {
            Channel channel = this.channelsRepository.All().FirstOrDefault(c => c.Id == id);
            this.channelsRepository.Delete(channel);
            await this.channelsRepository.SaveChangesAsync();
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

        public T GetById<T>(int id)
        {
            return this.channelsRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public string GetChannelNameById(int channelId)
        {
            return this.channelsRepository
                .All()
                .FirstOrDefault(c => c.Id == channelId)
                .Name;
        }

        public bool IsUserInChannel(string userId, int channelId)
        {
            if (!this.userChannelsRepository.All().Any(uc => uc.UserId == userId && uc.ChannelId == channelId))
            {
                return false;
            }

            return true;
        }

        public async Task JoinChannelAsync(JoinChannelInputModel input)
        {
            Channel channel = this.channelsRepository.All().FirstOrDefault(c => c.Code == input.Code);

            if (channel == null)
            {
                throw new InvalidOperationException("Channel with such a code does not exist!");
            }

            if (this.userChannelsRepository.All().Any(uc => uc.UserId == input.UserId && uc.ChannelId == channel.Id))
            {
                throw new InvalidOperationException("You have already joined this channel!");
            }

            UserChannel userChannel = new UserChannel
            {
                ChannelId = channel.Id,
                UserId = input.UserId,
            };

            await this.userChannelsRepository.AddAsync(userChannel);
            await this.userChannelsRepository.SaveChangesAsync();
        }
    }
}
