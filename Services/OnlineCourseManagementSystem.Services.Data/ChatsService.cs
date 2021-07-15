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
    using OnlineCourseManagementSystem.Web.ViewModels.Chats;

    public class ChatsService : IChatsService
    {
        private readonly IDeletableEntityRepository<Chat> chatRepository;
        private readonly IDeletableEntityRepository<ChatUser> chatUserRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ChatsService(IDeletableEntityRepository<Chat> chatRepository, IDeletableEntityRepository<ChatUser> chatUserRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.chatRepository = chatRepository;
            this.chatUserRepository = chatUserRepository;
            this.userRepository = userRepository;
        }

        public async Task CreateAsync(CreateChatInputModel inputModel)
        {
            Chat chat = new Chat()
            {
                Name = string.Empty,
            };

            ChatUser userCreator = new ChatUser
            {
                UserId = inputModel.CreatorId,
            };

            List<string> friendsName = new List<string>();
            foreach (var friendId in inputModel.FriendsToAdd)
            {
                ApplicationUser friend = this.userRepository.All().FirstOrDefault(c => c.Id == friendId);
                string friendName = $"{friend.FirstName} {friend.LastName}";
                friendsName.Add(friendName);
                chat.Users.Add(userCreator);
                ChatUser userAdded = new ChatUser
                {
                    UserId = friendId,
                };

                ApplicationUser user = this.userRepository.All().FirstOrDefault(u => u.Id == friendId);
                chat.Users.Add(userAdded);
            }

            chat.Name = string.Join(',', friendsName);

            await this.chatRepository.AddAsync(chat);
            await this.chatRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllBy<T>(string userId)
        {
            return this.chatUserRepository
                .All()
                .Where(x => x.UserId == userId && x.IsPinned == false)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllPinnedBy<T>(string userId)
        {
            return this.chatUserRepository
                .All()
                .Where(c => c.IsPinned && c.UserId == userId)
                .To<T>()
                .ToList();
        }

        public string GetNameBy(int? chatId)
        {
            return this.chatRepository
                .AllAsNoTracking()
                .FirstOrDefault(c => c.Id == chatId)
                .Name;
        }

        public IEnumerable<T> GetUsersByChat<T>(int chatId)
        {
            return this.chatUserRepository
                .All()
                .Where(x => x.ChatId == chatId)
                .To<T>()
                .ToList();
        }

        public async Task LeaveChat(int chatId, string userId)
        {
            ChatUser chatUser = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);
            this.chatUserRepository.Delete(chatUser);
            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task MuteChat(int chatId, string userId)
        {
            ChatUser chat = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);

            chat.IsMuted = true;

            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task PinChat(int chatId, string userId)
        {
            ChatUser chat = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);

            chat.IsPinned = true;

            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task UnmuteChat(int chatId, string userId)
        {
            ChatUser chat = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);

            chat.IsMuted = false;

            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task UnPinChat(int chatId, string userId)
        {
            ChatUser chat = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);

            chat.IsPinned = false;

            await this.chatUserRepository.SaveChangesAsync();
        }
    }
}
