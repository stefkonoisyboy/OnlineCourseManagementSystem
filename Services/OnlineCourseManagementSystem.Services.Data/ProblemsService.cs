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
    using OnlineCourseManagementSystem.Web.ViewModels.Problems;

    public class ProblemsService : IProblemsService
    {
        private readonly IDeletableEntityRepository<Problem> problemsRepository;

        public ProblemsService(IDeletableEntityRepository<Problem> problemsRepository)
        {
            this.problemsRepository = problemsRepository;
        }

        public async Task CreateAsync(CreateProblemInputModel input)
        {
            Problem problem = new Problem
            {
                Name = input.Name,
                Points = input.Points,
                ContestId = input.ContestId,
            };

            await this.problemsRepository.AddAsync(problem);
            await this.problemsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Problem problem = this.problemsRepository.All().FirstOrDefault(p => p.Id == id);
            this.problemsRepository.Delete(problem);
            await this.problemsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByContestIdViewModel<T>(int contestId, int problemId, int id, int itemsPerPage = 10)
        {
            return this.problemsRepository
                .All()
                .Where(p => p.ContestId == contestId && p.Id != problemId)
                .Skip((id - 1) * itemsPerPage).Take(itemsPerPage)
                .OrderBy(p => p.Id)
                .To<T>()
                .ToList();
        }

        public int GetAllProblemsCountByContestId(int contestId)
        {
            return this.problemsRepository
                .All()
                .Count(p => p.ContestId == contestId);
        }

        public T GetById<T>(int id)
        {
            return this.problemsRepository
                .All()
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetContestIdByProblemId(int problemId)
        {
            return this.problemsRepository
                .All()
                .FirstOrDefault(p => p.Id == problemId)
                .ContestId;
        }

        public async Task UpdateAsync(EditProblemInputModel input)
        {
            Problem problem = this.problemsRepository.All().FirstOrDefault(p => p.Id == input.Id);

            problem.Name = input.Name;
            problem.Points = input.Points;

            await this.problemsRepository.SaveChangesAsync();
        }
    }
}
