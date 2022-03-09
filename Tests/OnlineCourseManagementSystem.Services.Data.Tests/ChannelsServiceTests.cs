namespace OnlineCourseManagementSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data.Tests.Common;
    using OnlineCourseManagementSystem.Web.ViewModels.Channels;
    using Xunit;

    public class ChannelsServiceTests
    {
        public ChannelsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1", 1)]
        public void IsUserInChannel_Should_Work_Correctly(string userId, int channelId)
        {
            // Arrange
            List<UserChannel> testData = this.GetUserChannelsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserChannel>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChannelsService channelsService = new ChannelsService(null, mockedRepository.Object);
            bool expected = true;

            // Act
            bool actual = channelsService.IsUserInChannel(userId, channelId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetChannelNameById_Should_Work_Correctly(int channelId)
        {
            // Arrange
            List<Channel> testData = this.GetChannelsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Channel>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChannelsService channelsService = new ChannelsService(mockedRepository.Object, null);
            string expectedName = testData.FirstOrDefault(x => x.Id == channelId).Name;

            // Act
            string actual = channelsService.GetChannelNameById(channelId);

            // Assert
            Assert.Equal(expectedName, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int channelId)
        {
            // Arrange
            List<Channel> testData = this.GetChannelsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Channel>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChannelsService channelsService = new ChannelsService(mockedRepository.Object, null);
            string expectedName = testData.FirstOrDefault(x => x.Id == channelId).Name;
            string expectedCode = testData.FirstOrDefault(x => x.Id == channelId).Code;

            // Act
            ChannelByIdViewModel actual = channelsService.GetById<ChannelByIdViewModel>(channelId);

            // Assert
            Assert.Equal(expectedName, actual.Name);
            Assert.Equal(expectedCode, actual.Code);
        }

        [Theory]
        [InlineData("1")]
        public void GetAllByParticipantId_Should_Work_Correctly(string participantId)
        {
            // Arrange
            List<Channel> testData = this.GetChannelsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Channel>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChannelsService channelsService = new ChannelsService(mockedRepository.Object, null);
            IEnumerable<string> expectedNames = testData.Where(x => x.Users.Any(u => u.UserId == participantId)).OrderByDescending(x => x.CreatedOn).Select(x => x.Name).ToList();
            IEnumerable<string> expectedCodes = testData.Where(x => x.Users.Any(u => u.UserId == participantId)).OrderByDescending(x => x.CreatedOn).Select(x => x.Code).ToList();

            // Act
            IEnumerable<ChannelByIdViewModel> actual = channelsService.GetAllByParticipantId<ChannelByIdViewModel>(participantId);

            // Assert
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedCodes, actual.Select(x => x.Code));
        }

        [Theory]
        [InlineData("miro")]
        public void GetAllByCreatorId_Should_Work_Correctly(string creatorId)
        {
            // Arrange
            List<Channel> testData = this.GetChannelsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Channel>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChannelsService channelsService = new ChannelsService(mockedRepository.Object, null);
            IEnumerable<string> expectedNames = testData.Where(x => x.CreatorId == creatorId).OrderByDescending(x => x.CreatedOn).Select(x => x.Name).ToList();
            IEnumerable<string> expectedCodes = testData.Where(x => x.CreatorId == creatorId).OrderByDescending(x => x.CreatedOn).Select(x => x.Code).ToList();

            // Act
            IEnumerable<ChannelByIdViewModel> actual = channelsService.GetAllByCreatorId<ChannelByIdViewModel>(creatorId);

            // Assert
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedCodes, actual.Select(x => x.Code));
        }

        private List<Channel> GetChannelsTestData()
        {
            List<Channel> channels = new List<Channel>()
            {
                new Channel
                {
                    Id = 1,
                    Name = "C# Database Advanced",
                    Code = "c#-db",
                    Users = new List<UserChannel>()
                    {
                        new UserChannel
                        {
                            UserId = "1",
                            ChannelId = 1,
                        },
                    },
                    CreatorId = "miro",
                },
                new Channel
                {
                    Id = 2,
                    Name = "C# Database Basics",
                    Code = "c#-db",
                    Users = new List<UserChannel>()
                    {
                        new UserChannel
                        {
                            UserId = "2",
                            ChannelId = 2,
                        },
                    },
                    CreatorId = "miro",
                },
            };

            return channels;
        }

        private List<UserChannel> GetUserChannelsTestData()
        {
            List<UserChannel> userChannels = new List<UserChannel>()
            {
                new UserChannel
                {
                    UserId = "1",
                    ChannelId = 1,
                },
                new UserChannel
                {
                    UserId = "2",
                    ChannelId = 2,
                },
            };

            return userChannels;
        }
    }
}
