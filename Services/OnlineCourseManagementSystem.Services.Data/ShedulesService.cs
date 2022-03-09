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
    using OnlineCourseManagementSystem.Web.ViewModels.Shedules;

    public class ShedulesService : IShedulesServices
    {
        private readonly IDeletableEntityRepository<Shedule> sheduleRepository;
        private readonly IDeletableEntityRepository<Event> eventRepository;

        public ShedulesService(IDeletableEntityRepository<Shedule> sheduleRepository, IDeletableEntityRepository<Event> eventRepository)
        {
            this.sheduleRepository = sheduleRepository;
            this.eventRepository = eventRepository;
        }

        public async Task CreateAsync(CreateSheduleInputModel inputModel)
        {
            Event @event = this.eventRepository.All().FirstOrDefault(e => e.Id == inputModel.EventId);
            Shedule shedule = new Shedule()
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                StartTime = inputModel.StartTime,
                Duration = inputModel.Duration,
                EventId = inputModel.EventId,
            };

            if (shedule.StartTime < @event.StartDate)
            {
                throw new ArgumentException("StartTime should be less than the Event StartDate.");
            }

            if (shedule.StartTime.AddMinutes(inputModel.Duration) > @event.EndDate)
            {
                throw new ArgumentException("EndTime should be less than the Event EndDate.");
            }

            await this.sheduleRepository.AddAsync(shedule);
            await this.sheduleRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByEventId<T>(int eventId)
        {
            return this.sheduleRepository
                .All()
                .Where(s => s.EventId == eventId)
                .To<T>();
        }
    }
}
