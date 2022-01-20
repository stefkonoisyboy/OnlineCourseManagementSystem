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
    using OnlineCourseManagementSystem.Web.ViewModels.Messages;
    using Xunit;

    public class MessagesServiceTests
    {
        public MessagesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1)]
        public void GetAllBy_Should_Work_Correctly(int chatId)
        {
            // Arrange
            List<Message> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Message>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessagesService messagesService = new MessagesService(null, mockedRepository.Object);

            IEnumerable<int> expectedIds = testData.Where(m => m.ChatId == chatId).OrderBy(m => m.CreatedOn).Select(m => m.Id).ToList();
            IEnumerable<string> expectedContents = testData.Where(m => m.ChatId == chatId).OrderBy(m => m.CreatedOn).Select(m => m.Content).ToList();
            IEnumerable<string> expectedCreatorIds = testData.Where(m => m.ChatId == chatId).OrderBy(m => m.CreatedOn).Select(m => m.CreatorId).ToList();

            // Act
            IEnumerable<MessageViewModel> actual = messagesService.GetAllBy<MessageViewModel>(chatId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.Id));
            Assert.Equal(expectedContents, actual.Select(a => a.Content));
            Assert.Equal(expectedCreatorIds, actual.Select(a => a.CreatorId));
        }

        [Theory]
        [InlineData(1)]
        public void GetMessageBy_Should_Work_Correctly(int messageId)
        {
            // Arrange
            List<Message> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Message>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessagesService messagesService = new MessagesService(null, mockedRepository.Object);

            string expectedContent = testData.FirstOrDefault(m => m.Id == messageId).Content;
            string expectedCreatorId = testData.FirstOrDefault(m => m.Id == messageId).CreatorId;

            // Act
            MessageViewModel actual = messagesService.GetMessageBy<MessageViewModel>(messageId);

            // Arrange
            Assert.Equal(expectedContent, actual.Content);
            Assert.Equal(expectedCreatorId, actual.CreatorId);
        }

        [Theory]
        [InlineData(1, "con")]
        public void SeacrheMessages_Should_Work_Correctly(int chatId, string input)
        {
            // Arrange
            List<Message> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Message>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessagesService messagesService = new MessagesService(null, mockedRepository.Object);

            IEnumerable<int> expectedIds = testData.Where(m => m.ChatId == chatId && m.Content.Contains(input)).Select(m => m.Id).ToList();
            IEnumerable<string> expectedContents = testData.Where(m => m.ChatId == chatId && m.Content.Contains(input)).Select(m => m.Content).ToList();
            IEnumerable<string> expectedCreatorIds = testData.Where(m => m.ChatId == chatId && m.Content.Contains(input)).Select(m => m.CreatorId).ToList();
            SearchInputModel inputModel = new SearchInputModel()
            {
                ChatId = chatId,
                Input = input,
            };

            // Act
            IEnumerable<MessageViewModel> actual = messagesService.SearchMessages<MessageViewModel>(inputModel);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.Id));
            Assert.Equal(expectedContents, actual.Select(a => a.Content));
            Assert.Equal(expectedCreatorIds, actual.Select(a => a.CreatorId));
        }

        private List<Message> GetTestData()
        {
            return new List<Message>()
            {
                new Message()
                {
                    Id = 1,
                    Content = "Some conntent message1",
                    Creator = new ApplicationUser()
                    {
                        Id = "3",
                        FirstName = "Gosho",
                        LastName = "Konev",
                    },
                    CreatorId = "3",
                    ChatId = 1,
                },
                new Message()
                {
                    Id = 2,
                    Content = "Some conntent message2",
                    Creator = new ApplicationUser()
                    {
                        Id = "1",
                        FirstName = "Gosho",
                        LastName = "Konev",
                    },
                    CreatorId = "1",
                    ChatId = 2,
                },
                new Message()
                {
                    Id = 3,
                    Content = "Some conntent message3",
                    Creator = new ApplicationUser()
                    {
                        Id = "2",
                        FirstName = "Pesho",
                        LastName = "Konev",
                    },
                    CreatorId = "2",
                    ChatId = 1,
                },
                new Message()
                {
                    Id = 4,
                    Creator = new ApplicationUser()
                    {
                        Id = "1",
                        FirstName = "Gosho",
                        LastName = "Konev",
                    },
                    Content = "Some conntent message4",
                    CreatorId = "1",
                    ChatId = 3,
                },
            };
        }
    }
}
