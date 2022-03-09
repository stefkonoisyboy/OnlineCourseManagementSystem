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
    using OnlineCourseManagementSystem.Web.ViewModels.MessageQAs;
    using Xunit;

    public class MessageQAsServiceTests
    {
        public MessageQAsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1", 1)]
        public void HasUserLikedMessage_Should_Work_Correctly(string creatorId, int messageId)
        {
            // Arrange
            List<Like> testData = this.GetLikesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Like>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(null, mockedRepository.Object);
            bool expected = true;

            // Act
            bool actual = messageQAsService.HasUserLikedMessage(creatorId, messageId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetStarredStatus_Should_Work_Correctly(int messageId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            bool expected = false;

            // Act
            bool actual = messageQAsService.GetStarredStatus(messageId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetHighlightedStatus_Should_Work_Correctly(int messageId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            bool expected = false;

            // Act
            bool actual = messageQAsService.GetHighlightedStatus(messageId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetAnsweredStatus_Should_Work_Correctly(int messageId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            bool expected = false;

            // Act
            bool actual = messageQAsService.GetAnsweredStatus(messageId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetCountOfRepliesForGivenMessage(int messageId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            int expected = testData.Count(m => m.ParentId == messageId);

            // Act
            int actual = messageQAsService.GetCountOfRepliesForGivenMessage(messageId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int messageId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            int expectedId = testData.FirstOrDefault(x => x.Id == messageId).Id;
            string expectedContent = testData.FirstOrDefault(x => x.Id == messageId).Content;

            // Act
            AllMessagesByChannelIdViewModel actual = messageQAsService.GetById<AllMessagesByChannelIdViewModel>(messageId);

            // Assert
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedContent, actual.Content);
        }

        [Theory]
        [InlineData(1)]
        public void GetAllMessagesByChannelId_Should_Work_Correctly(int channelId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            IEnumerable<int> expectedIds = testData.Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && !m.IsArchived).OrderByDescending(m => m.IsHighlighted).ThenByDescending(m => m.Likes.Count()).ThenByDescending(m => m.ModifiedOn).Select(x => x.Id).ToList();
            IEnumerable<string> expectedContents = testData.Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && !m.IsArchived).OrderByDescending(m => m.IsHighlighted).ThenByDescending(m => m.Likes.Count()).ThenByDescending(m => m.ModifiedOn).Select(x => x.Content).ToList();

            // Act
            IEnumerable<AllMessagesByChannelIdViewModel> actual = messageQAsService.GetAllMessagesByChannelId<AllMessagesByChannelIdViewModel>(channelId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllArchivedMessagesByChannelId_Should_Work_Correctly(int channelId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            IEnumerable<int> expectedIds = testData.OrderByDescending(m => m.IsHighlighted)
                                                    .ThenByDescending(m => m.ModifiedOn)
                                                    .Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && m.IsArchived)
                                                    .Select(x => x.Id)
                                                    .ToList();
            IEnumerable<string> expectedContents = testData.OrderByDescending(m => m.IsHighlighted)
                                                    .ThenByDescending(m => m.ModifiedOn)
                                                    .Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && m.IsArchived)
                                                    .Select(x => x.Content)
                                                    .ToList();

            // Act
            IEnumerable<AllMessagesByChannelIdViewModel> actual = messageQAsService.GetAllArchivedMessagesByChannelId<AllMessagesByChannelIdViewModel>(channelId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllRecentMessagesByChannelId_Should_Work_Correctly(int channelId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            IEnumerable<int> expectedIds = testData.OrderByDescending(m => m.CreatedOn)
                                                    .Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && !m.IsArchived)
                                                    .Select(x => x.Id)
                                                    .ToList();
            IEnumerable<string> expectedContents = testData.OrderByDescending(m => m.CreatedOn)
                                                    .Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && !m.IsArchived)
                                                    .Select(x => x.Content)
                                                    .ToList();

            // Act
            IEnumerable<AllMessagesByChannelIdViewModel> actual = messageQAsService.GetAllRecentMessagesByChannelId<AllMessagesByChannelIdViewModel>(channelId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllRepliesByParentId_Should_Work_Correctly(int parentId)
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            IEnumerable<int> expectedIds = testData.Where(r => r.ParentId == parentId)
                                                    .OrderByDescending(r => r.CreatedOn)
                                                    .Select(x => x.Id)
                                                    .ToList();
            IEnumerable<string> expectedContents = testData.Where(r => r.ParentId == parentId)
                                                    .OrderByDescending(r => r.CreatedOn)
                                                    .Select(x => x.Content)
                                                    .ToList();

            // Act
            IEnumerable<AllMessagesByChannelIdViewModel> actual = messageQAsService.GetAllRepliesByParentId<AllMessagesByChannelIdViewModel>(parentId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
        }

        [Fact]
        public void GetAllReplies_Should_Work_Correctly()
        {
            // Arrange
            List<MessageQA> testData = this.GetMessageQAsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MessageQA>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessageQAsService messageQAsService = new MessageQAsService(mockedRepository.Object, null);
            IEnumerable<int> expectedIds = testData.Where(r => r.ParentId != null)
                                                    .OrderByDescending(r => r.CreatedOn)
                                                    .Select(x => x.Id)
                                                    .ToList();
            IEnumerable<string> expectedContents = testData.Where(r => r.ParentId != null)
                                                    .OrderByDescending(r => r.CreatedOn)
                                                    .Select(x => x.Content)
                                                    .ToList();

            // Act
            IEnumerable<AllMessagesByChannelIdViewModel> actual = messageQAsService.GetAllReplies<AllMessagesByChannelIdViewModel>();

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
        }

        private List<MessageQA> GetMessageQAsTestData()
        {
            List<MessageQA> messageQAs = new List<MessageQA>()
            {
                new MessageQA
                {
                    Id = 1,
                    Content = "Some content",
                    Creator = new ApplicationUser
                    {
                        Id = "1",
                        FirstName = "Miro",
                        LastName = "Uzunov",
                        ProfileImageUrl = "remote",
                    },
                    CreatorId = "1",
                    CreatedOn = DateTime.UtcNow,
                    ChannelId = 1,
                },
                new MessageQA
                {
                    Id = 2,
                    Content = "Some content",
                    Creator = new ApplicationUser
                    {
                        Id = "2",
                        FirstName = "Stefko",
                        LastName = "Tsonyovski",
                        ProfileImageUrl = "remote",
                    },
                    CreatorId = "2",
                    CreatedOn = DateTime.UtcNow,
                    ChannelId = 1,
                },
            };

            return messageQAs;
        }

        private List<Like> GetLikesTestData()
        {
            List<Like> likes = new List<Like>()
            {
                new Like
                {
                    Id = 1,
                    CreatorId = "1",
                    MessageQAId = 1,
                },
                new Like
                {
                    Id = 2,
                    CreatorId = "2",
                    MessageQAId = 1,
                },
            };

            return likes;
        }
    }
}
