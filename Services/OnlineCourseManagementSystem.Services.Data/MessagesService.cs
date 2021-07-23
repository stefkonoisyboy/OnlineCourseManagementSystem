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

        public int AllUnseenMessagesCountBy(int chatId, int userId)
        {
            return this.messageRepository.All()
                .Where(m => m.IsSeen == false && m.ChatId == chatId)
                .Count();
        }

        public async Task<int> Create(CreateMessageInputModel inputModel)
        {
            Message message = new Message
            {
                CreatorId = inputModel.UserId,
                Content = inputModel.Content,
                ChatId = (int)inputModel.ChatId,
            };

            await this.messageRepository.AddAsync(message);
            await this.messageRepository.SaveChangesAsync();

            return message.Id;
        }

        public async Task DeleteAsync(int messageId)
        {
            Message message = this.messageRepository.All().FirstOrDefault(x => x.Id == messageId);

            this.messageRepository.HardDelete(message);
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

        public T GetMessageBy<T>(int messageId)
        {
            return this.messageRepository
                .All()
                .Where(m => m.Id == messageId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task MarkAsSeeen(int messageId)
        {
            Message message = this.messageRepository.All().FirstOrDefault(m => m.Id == messageId);

            message.IsSeen = true;

            await this.messageRepository.SaveChangesAsync();
        }

        public IEnumerable<T> SearchMessages<T>(SearchInputModel inputModel)
        {
            return this.messageRepository
                .All()
                .Where(m => m.ChatId == inputModel.ChatId && m.Content.Contains(inputModel.Input))
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(EditMessageInputModel inputModel)
        {
            Message message = this.messageRepository.All().FirstOrDefault(m => m.Id == inputModel.Id);

            message.Content = inputModel.Content;

            await this.messageRepository.SaveChangesAsync();
        }
    }
}
