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
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Data.Tests.Common;
    using OnlineCourseManagementSystem.Web.ViewModels.Chats;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using Xunit;

    public class ChatsServiceTests
    {
        public ChatsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("2")]
        public void GetAllBy_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<ChatUser> testData = this.GetChatUserTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ChatUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChatsService chatsService = new ChatsService(null, mockedRepository.Object, null, null);

            IEnumerable<int> expectedChatIds = testData.Where(cu => cu.UserId == userId && cu.IsPinned == false).Select(cu => cu.ChatId).ToList();
            IEnumerable<string> expectedCreatorIds = testData.Where(cu => cu.UserId == userId && cu.IsPinned == false).Select(cu => cu.Chat.CreatorId).ToList();

            // Act
            IEnumerable<ChatViewModel> actual = chatsService.GetAllBy<ChatViewModel>(userId);

            // Arrange
            Assert.Equal(expectedChatIds, actual.Select(a => a.ChatId));
            Assert.Equal(expectedCreatorIds, actual.Select(a => a.CreatorId));
        }

        [Theory]
        [InlineData(1)]
        public void GetCreatorIdBy_Should_Work_Correctly(int chatId)
        {
            // Arrange
            List<Chat> testData = this.GetChatTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Chat>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChatsService chatsService = new ChatsService(mockedRepository.Object, null, null, null);

            string expectedCreatorId = testData.FirstOrDefault(c => c.Id == chatId).CreatorId;

            // Act
            string actual = chatsService.GetCreatorIdBy(chatId);

            // Arrange
            Assert.Equal(expectedCreatorId, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetBy_Soud_Work_Correctly(int chatId)
        {
            List<ChatUser> testData = this.GetChatUserTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ChatUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChatsService chatsService = new ChatsService(null, mockedRepository.Object, null, null);
            string expectedCreatorId = testData.FirstOrDefault(c => c.Id == chatId).Chat.CreatorId;
            string expectedIconUrl = testData.FirstOrDefault(c => c.Id == chatId).Chat.IconRemoteUrl;

            // Act
            ChatViewModel actual = chatsService.GetBy<ChatViewModel>(chatId);

            // Arrange
            Assert.Equal(expectedIconUrl, actual.IconUrl);
            Assert.Equal(expectedCreatorId, actual.CreatorId);
        }

        [Theory]
        [InlineData(1)]
        public void GetUsersByChat_Should_Work_Correctly(int chatId)
        {
            // Arrange
            List<ChatUser> testData = this.GetChatUserTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ChatUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChatsService chatsService = new ChatsService(null, mockedRepository.Object, null, null);

            IEnumerable<string> expectedUserIds = testData
                .Where(c => c.ChatId == chatId)
                .OrderBy(c => c.User.FirstName)
                .ThenBy(c => c.User.LastName)
                .Select(c => c.UserId).ToList();
            IEnumerable<string> expectedFirstNames = testData.Where(c => c.ChatId == chatId).Select(c => c.User.FirstName).ToList();
            IEnumerable<string> expectedLastNames = testData.Where(c => c.ChatId == chatId).Select(c => c.User.LastName).ToList();

            // Act
            IEnumerable<UserViewModel> actual = chatsService.GetUsersByChat<UserViewModel>(chatId);

            // Arrange
            Assert.Equal(expectedUserIds, actual.Select(c => c.Id));
            Assert.Equal(expectedFirstNames, actual.Select(c => c.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(c => c.LastName));
        }

        [Theory]
        [InlineData("2")]
        public void GetAllPinnedBy_Should_Work_Corrctly(string userId)
        {
            // Arrange
            List<ChatUser> testData = this.GetChatUserTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ChatUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChatsService chatsService = new ChatsService(null, mockedRepository.Object, null, null);

            IEnumerable<int> expectIds = testData.Where(c => c.IsPinned && c.UserId == userId).Select(c => c.ChatId).ToList();
            IEnumerable<string> expectedCreatorIds = testData.Where(c => c.IsPinned && c.UserId == userId).Select(c => c.Chat.CreatorId).ToList();

            // Act
            IEnumerable<ChatViewModel> actual = chatsService.GetAllPinnedBy<ChatViewModel>(userId);

            // Act
            Assert.Equal(expectIds, actual.Select(c => c.ChatId));
            Assert.Equal(expectedCreatorIds, actual.Select(c => c.CreatorId));
        }

        [Theory]
        [InlineData("2", 3)]
        public void GetIconUrl_Should_Work_Correctly(string userId, int chatId)
        {
            // Arrange
            List<ChatUser> testData = this.GetChatUserTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ChatUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChatsService chatsService = new ChatsService(null, mockedRepository.Object, null, null);
            string expectedUrl = testData.FirstOrDefault(c => c.ChatId == chatId && c.UserId != userId && c.Chat.ChatType != ChatType.GroupChat).User.ProfileImageUrl;

            // Act
            string actual = chatsService.GetIconUrl(userId, chatId);

            // Arrange
            Assert.Equal(expectedUrl, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        public void GetByCurrentUser_Should_Work_Correctly(string userId, int chatId)
        {
            // Arrange
            List<ChatUser> testData = this.GetChatUserTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ChatUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChatsService chatsService = new ChatsService(null, mockedRepository.Object, null, null);

            string expectedCreatorId = testData.FirstOrDefault(cu => cu.UserId == userId && cu.ChatId == chatId).Chat.CreatorId;
            string expectedIconUrl = testData.FirstOrDefault(cu => cu.UserId == userId && cu.ChatId == chatId).Chat.IconRemoteUrl;

            // Act
            ChatViewModel actual = chatsService.GetByCurrentUser<ChatViewModel>(userId, chatId);

            // Arrange
            Assert.Equal(expectedCreatorId, actual.CreatorId);
            Assert.Equal(expectedIconUrl, actual.IconUrl);
        }

        private List<Chat> GetChatTestData()
        {
            return new List<Chat>()
            {
                new Chat()
                {
                     Id = 1,
                     CreatorId = "2",
                },
                new Chat()
                {
                    Id = 2,
                    CreatorId = "1",
                },
                new Chat()
                {
                    Id = 3,
                    CreatorId = "2",
                },
            };
        }

        private List<ChatUser> GetChatUserTestData()
        {
            return new List<ChatUser>()
            {
                new ChatUser()
                {
                    Id = 1,
                    IsPinned = true,
                    ChatId = 1,
                    Chat = new Chat()
                    {
                        Id = 1,
                        CreatorId = "2",
                        ChatType = ChatType.GroupChat,
                        IconRemoteUrl = "iconurl1",
                    },
                    User = new ApplicationUser()
                    {
                        Id = "2",
                        FirstName = "Josh",
                        LastName = "Noev",
                    },
                    UserId = "2",
                },
                new ChatUser()
                {
                    Id = 2,
                    IsPinned = false,
                    ChatId = 1,
                    Chat = new Chat()
                    {
                        Id = 1,
                        CreatorId = "2",
                        ChatType = ChatType.GroupChat,
                        IconRemoteUrl = "iconurl1",
                    },
                    User = new ApplicationUser()
                    {
                        Id = "1",
                        FirstName = "Tob",
                        LastName = "Zong",
                    },
                    UserId = "1",
                },
                new ChatUser()
                {
                    Id = 3,
                    IsPinned = true,
                    ChatId = 3,
                    Chat = new Chat()
                    {
                        Id = 3,
                        CreatorId = "2",
                        ChatType = ChatType.NormalChat,
                        IconRemoteUrl = "iconurl2",
                    },
                    User = new ApplicationUser()
                    {
                        Id = "2",
                        FirstName = "Josh",
                        LastName = "Noev",
                        ProfileImageUrl = "url2",
                    },
                    UserId = "2",
                },
                new ChatUser()
                {
                    Id = 4,
                    ChatId = 3,
                    Chat = new Chat()
                    {
                        Id = 3,
                        CreatorId = "2",
                        ChatType = ChatType.NormalChat,
                        IconRemoteUrl = "iconurl2",
                    },
                    User = new ApplicationUser()
                    {
                        Id = "3",
                        FirstName = "Josh",
                        LastName = "Petrov",
                        ProfileImageUrl = "url3",
                    },
                    UserId = "3",
                },
            };
        }
    }
}
