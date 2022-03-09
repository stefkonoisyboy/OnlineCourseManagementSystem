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
    using OnlineCourseManagementSystem.Web.ViewModels.Contests;

    public class ContestsService : IContestsService
    {
        private readonly IDeletableEntityRepository<Contest> contestsRepository;

        public ContestsService(IDeletableEntityRepository<Contest> contestsRepository)
        {
            this.contestsRepository = contestsRepository;
        }

        public async Task CreateAsync(CreateContestInputModel input)
        {
            Contest contest = new Contest
            {
                Name = input.Name,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
            };

            await this.contestsRepository.AddAsync(contest);
            await this.contestsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Contest contest = this.contestsRepository.All().FirstOrDefault(c => c.Id == id);
            this.contestsRepository.Delete(contest);
            await this.contestsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int id, int itemsPerPage = 10)
        {
            return this.contestsRepository
                .All()
                .Skip((id - 1) * itemsPerPage).Take(itemsPerPage)
                .OrderByDescending(c => c.StartDate)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllActive<T>()
        {
            return this.contestsRepository
                .All()
                .Where(c => DateTime.UtcNow > c.StartDate && DateTime.UtcNow < c.EndDate)
                .OrderByDescending(c => c.StartDate)
                .To<T>()
                .ToList();
        }

        public int GetAllContestsCount()
        {
            return this.contestsRepository
                .All()
                .Count();
        }

        public IEnumerable<T> GetAllFinished<T>()
        {
            return this.contestsRepository
                .All()
                .Where(c => DateTime.UtcNow > c.EndDate)
                .OrderByDescending(c => c.StartDate)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.contestsRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public string GetContestNameById(int id)
        {
            return this.contestsRepository
                .All()
                .FirstOrDefault(c => c.Id == id)
                .Name;
        }

        public async Task UpdateAsync(EditContestInputModel input)
        {
            Contest contest = this.contestsRepository.All().FirstOrDefault(c => c.Id == input.Id);

            contest.Name = input.Name;
            contest.StartDate = input.StartDate;
            contest.EndDate = input.EndDate;

            await this.contestsRepository.SaveChangesAsync();
        }
    }
}
