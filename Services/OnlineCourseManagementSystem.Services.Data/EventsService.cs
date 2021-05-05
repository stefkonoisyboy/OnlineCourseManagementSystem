using CloudinaryDotNet;
using OnlineCourseManagementSystem.Data.Common.Repositories;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using OnlineCourseManagementSystem.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public class EventsService : IEventsService
    {
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly CloudinaryService cloudinaryService;
        private const string FILES_FOLDER = "events";

        public EventsService(IDeletableEntityRepository<Event> eventRepository, Cloudinary cloudinaryUtility)
        {
            this.eventRepository = eventRepository;
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
                Description = inputModel.Description,
            };

            foreach (var file in inputModel.Files)
            {
                string extension = System.IO.Path.GetExtension(file.FileName);
                string fileName = $"Events_{Guid.NewGuid()}" + extension;
                string remoteUrl = await this.cloudinaryService.UploadFile(file, fileName, extension, FILES_FOLDER);

                File uploadFile = new File
                {
                    Extension = extension,
                    RemoteUrl = remoteUrl,
                };

                @event.Files.Add(uploadFile);
            }

            await this.eventRepository.AddAsync(@event);
            await this.eventRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.eventRepository
                .All()
                .OrderBy(x => x.StartDate)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int eventId)
        {
            return this.eventRepository
                .All()
                .Where(e => e.Id == eventId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task Approve(int eventId)
        {
            Event @event = this.eventRepository
                .All()
                .FirstOrDefault(e => e.Id == eventId);

            @event.IsApproved = true;

            await this.eventRepository.SaveChangesAsync();
        }

        public async Task Disapprove(int eventId)
        {
            Event @event = this.eventRepository
                .All()
                .FirstOrDefault(e => e.Id == eventId);

            @event.IsApproved = false;

            await this.eventRepository.SaveChangesAsync();
        }
    }
}
