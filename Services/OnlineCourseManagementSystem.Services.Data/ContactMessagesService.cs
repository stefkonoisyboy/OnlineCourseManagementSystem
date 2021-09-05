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
    using OnlineCourseManagementSystem.Web.ViewModels.ContactMessages;

    public class ContactMessagesService : IContactMessagesService
    {
        private readonly IDeletableEntityRepository<ContactMessage> contactMessageRepository;

        public ContactMessagesService(IDeletableEntityRepository<ContactMessage> contactMessageRepository)
        {
            this.contactMessageRepository = contactMessageRepository;
        }

        public async Task CreateAsync(CreateContactMessageInputModel inputModel)
        {
            ContactMessage contactMessage = new ContactMessage()
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Phone = inputModel.Phone,
                Content = inputModel.Content,
            };

            await this.contactMessageRepository.AddAsync(contactMessage);
            await this.contactMessageRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.contactMessageRepository
                .All()
                .To<T>()
                .ToList();
        }

        public async Task MarkAsSeen(MarkContactMessageAsSeenInputModel inputModel)
        {
            ContactMessage contactMessage = this.contactMessageRepository.All().FirstOrDefault(cm => cm.Id == inputModel.ContactMessageId);
            contactMessage.IsSeen = true;
            contactMessage.SeenByUserId = inputModel.UserId;

            this.contactMessageRepository.Update(contactMessage);
            await this.contactMessageRepository.SaveChangesAsync();
        }
    }
}
