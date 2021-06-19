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
    using OnlineCourseManagementSystem.Web.ViewModels.Messages;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Chat> chatRepository;
        private readonly IDeletableEntityRepository<Message> messageRepository;

        public MessagesService(IDeletableEntityRepository<Chat> chatRepository, IDeletableEntityRepository<Message> messageRepository)
        {
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
        }

        public async Task Create(CreateMessageInputModel inputModel)
        {
            Message message = new Message
            {
                CreatorId = inputModel.UserId,
                Content = inputModel.Content,
                ChatId = (int)inputModel.ChatId,
            };

            await this.messageRepository.AddAsync(message);
            await this.messageRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllBy<T>(int chatId)
        {
            return this.messageRepository
                .All()
                .Where(m => m.ChatId == chatId)
                .OrderBy(x => x.CreatedOn)
                .To<T>()
                .ToList();
        }
    }
}
