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

        public ChatsService(IDeletableEntityRepository<Chat> chatRepository, IDeletableEntityRepository<ChatUser> chatUserRepository ,IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.chatRepository = chatRepository;
            this.chatUserRepository = chatUserRepository;
            this.userRepository = userRepository;
        }

        public async Task CreateAsync(CreateChatInputModel inputModel)
        {
            Chat chat = new Chat();
            ChatUser userCreator = new ChatUser
            {
                UserId = inputModel.CreatorId,
            };

            ChatUser userAdded = new ChatUser
            {
                UserId = inputModel.AddedUserId,
            };

            ApplicationUser user = this.userRepository.All().FirstOrDefault(u => u.Id == inputModel.AddedUserId);

            chat.Name = $"{user.FirstName} {user.LastName}";
            chat.Users.Add(userCreator);
            chat.Users.Add(userAdded);

            await this.chatRepository.AddAsync(chat);
            await this.chatRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllBy<T>(string userId)
        {
            return this.chatUserRepository
                .All()
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();
        }
    }
}
