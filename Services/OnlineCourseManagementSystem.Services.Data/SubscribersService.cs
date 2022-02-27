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
    using OnlineCourseManagementSystem.Web.ViewModels.Subscribers;

    public class SubscribersService : ISubscribersService
    {
        private readonly IDeletableEntityRepository<Subscriber> subscribersRepository;

        public SubscribersService(IDeletableEntityRepository<Subscriber> subscribersRepository)
        {
            this.subscribersRepository = subscribersRepository;
        }

        public bool? CheckSubscribedByEmail(string email)
        {
            return this.subscribersRepository.All().FirstOrDefault(s => s.Email == email)?.IsConfirmed;
        }

        public async Task ConfirmSubscriptionAsync(ConfirmSubscriptionInputModel inputModel)
        {
            Subscriber subscriber = this.subscribersRepository.All().FirstOrDefault(sb => sb.Id == inputModel.Id);
            subscriber.IsConfirmed = true;
            subscriber.ConfirmedDate = DateTime.UtcNow;
            int minutes = DateTime.UtcNow.Subtract(subscriber.CreatedOn).Minutes;

            if (minutes > 15)
            {
                this.subscribersRepository.Delete(subscriber);
                throw new ArgumentException("Confirmation failed!");
            }
            else
            {
                this.subscribersRepository.Update(subscriber);
            }

            await this.subscribersRepository.SaveChangesAsync();
        }

        public async Task<string> CreateAsync(CreateSubscriberInputModel inputModel)
        {
            if (this.subscribersRepository.All().Any(sb => sb.Email == inputModel.Email))
            {
                throw new ArgumentException("Already existing eamil!Ask for help from Admin!");
            }

            Subscriber subscriber = new Subscriber()
            {
                Email = inputModel.Email,
            };

            await this.subscribersRepository.AddAsync(subscriber);
            await this.subscribersRepository.SaveChangesAsync();

            return subscriber.Id;
        }

        public T GetById<T>(string id)
        {
            T subscriber = this.subscribersRepository.All().Where(sb => sb.Id == id).To<T>().FirstOrDefault();
            if (subscriber == null)
            {
                throw new ArgumentException("Not found!");
            }

            return subscriber;
        }
    }
}
