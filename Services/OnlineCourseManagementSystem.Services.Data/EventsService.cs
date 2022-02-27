namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Ganss.XSS;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Events;

    public class EventsService : IEventsService
    {
        private const string FILESFOLDER = "events";
        private readonly IDeletableEntityRepository<Event> eventsRepository;
        private readonly CloudinaryService cloudinaryService;

        public EventsService(IDeletableEntityRepository<Event> eventRepository, Cloudinary cloudinaryUtility)
        {
            this.eventsRepository = eventRepository;
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
        }

        public async Task CreateAsync(CreateEventInputModel inputModel)
        {
            Event @event = new Event
            {
                Theme = inputModel.Theme,
                Address = inputModel.Address,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                CreatorId = inputModel.CreatorId,
                Description = new HtmlSanitizer().Sanitize(inputModel.Description),
            };

            if (inputModel.Files?.Count() > 0)
            {
                foreach (var file in inputModel.Files)
                {
                    string extension = System.IO.Path.GetExtension(file.FileName);
                    string fileName = $"Events_{Guid.NewGuid()}" + extension;
                    string remoteUrl = await this.cloudinaryService.UploadFile(file, fileName, extension, FILESFOLDER);

                    File uploadFile = new File
                    {
                        Extension = extension,
                        RemoteUrl = remoteUrl,
                    };

                    @event.Files.Add(uploadFile);
                }
            }

            await this.eventsRepository.AddAsync(@event);
            await this.eventsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllCreatedByUserId<T>(string userId)
        {
            return this.eventsRepository
                .All()
                .Where(e => e.CreatorId == userId)
                .OrderByDescending(e => e.CreatedOn)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int eventId)
        {
            return this.eventsRepository
                .All()
                .Where(e => e.Id == eventId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task ApproveAsync(int eventId)
        {
            Event @event = this.eventsRepository
                .All()
                .FirstOrDefault(e => e.Id == eventId);

            @event.IsApproved = true;

            await this.eventsRepository.SaveChangesAsync();
        }

        public async Task DisapproveAsync(int eventId)
        {
            Event @event = this.eventsRepository
                .All()
                .FirstOrDefault(e => e.Id == eventId);

            @event.IsApproved = false;

            await this.eventsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllComing<T>()
        {
            return this.eventsRepository
                .All()
                .Where(e => DateTime.UtcNow <= e.StartDate && e.IsApproved == true)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllFinished<T>()
        {
            return this.eventsRepository
                .All()
                .Where(e => DateTime.UtcNow >= e.EndDate && e.IsApproved == true)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.eventsRepository
                .All()
                .OrderByDescending(e => e.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByAdmin<T>()
        {
            return this.eventsRepository
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToList();
        }

        public async Task DeleteAsync(int eventId)
        {
            Event @event = this.eventsRepository.All().FirstOrDefault(e => e.Id == eventId);

            this.eventsRepository.Delete(@event);
            await this.eventsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditEventInputModel inputModel)
        {
            Event @event = this.eventsRepository.All().FirstOrDefault(e => e.Id == inputModel.Id);

            @event.Theme = inputModel.Theme;
            @event.StartDate = inputModel.StartDate;
            @event.EndDate = inputModel.EndDate;
            @event.Address = inputModel.Address;
            @event.Description = new HtmlSanitizer().Sanitize(inputModel.Description);

            if (inputModel.FilesToAdd != null)
            {
                foreach (var file in inputModel.FilesToAdd)
                {
                    string extension = System.IO.Path.GetExtension(file.FileName);
                    string fileName = $"Events_{Guid.NewGuid()}" + extension;
                    string remoteUrl = await this.cloudinaryService.UploadFile(file, fileName, extension, FILESFOLDER);

                    File uploadFile = new File
                    {
                        Extension = extension,
                        RemoteUrl = remoteUrl,
                    };

                    @event.Files.Add(uploadFile);
                }
            }

            this.eventsRepository.Update(@event);
            await this.eventsRepository.SaveChangesAsync();
        }
    }
}
