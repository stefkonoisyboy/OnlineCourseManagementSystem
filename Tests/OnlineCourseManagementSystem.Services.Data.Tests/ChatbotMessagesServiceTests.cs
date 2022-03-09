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
    using OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages;
    using Xunit;

    public class ChatbotMessagesServiceTests
    {
        public ChatbotMessagesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1")]
        public void GetAllByCreatorId_Should_Work_Correctly(string creatorId)
        {
            // Arrange
            List<ChatbotMessage> testData = this.GetChatbotMessagesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ChatbotMessage>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChatbotMessagesService chatbotMessagesService = new ChatbotMessagesService(mockedRepository.Object);
            IEnumerable<string> expectedContents = testData.Where(cm => cm.CreatorId == creatorId).OrderBy(cm => cm.CreatedOn).Select(cm => cm.Content).ToList();
            IEnumerable<string> expectedCreatorIds = testData.Where(cm => cm.CreatorId == creatorId).OrderBy(cm => cm.CreatedOn).Select(cm => cm.CreatorId).ToList();

            // Act
            IEnumerable<AllChatbotMessagesByCreatorIdViewModel> actual = chatbotMessagesService.GetAllByCreatorId<AllChatbotMessagesByCreatorIdViewModel>(creatorId);

            // Assert
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
            Assert.Equal(expectedCreatorIds, actual.Select(x => x.CreatorId));
        }

        private List<ChatbotMessage> GetChatbotMessagesTestData()
        {
            List<ChatbotMessage> chatbotMessages = new List<ChatbotMessage>()
            {
                new ChatbotMessage
                {
                    Id = 1,
                    Content = "Content",
                    CreatorId = "1",
                },
                new ChatbotMessage
                {
                    Id = 2,
                    Content = "Content",
                    CreatorId = "2",
                },
            };

            return chatbotMessages;
        }
    }
}
