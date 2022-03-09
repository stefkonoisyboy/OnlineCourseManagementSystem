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
    using OnlineCourseManagementSystem.Web.ViewModels.Tests;

    public class TestsService : ITestsService
    {
        private readonly IDeletableEntityRepository<Test> testsRepository;

        public TestsService(IDeletableEntityRepository<Test> testsRepository)
        {
            this.testsRepository = testsRepository;
        }

        public async Task CreateAsync(CreateTestInputModel input)
        {
            Test test = new Test
            {
                Input = input.TestInput,
                Output = input.ExpectedOutput,
                ProblemId = input.ProblemId,
            };

            await this.testsRepository.AddAsync(test);
            await this.testsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Test test = this.testsRepository.All().FirstOrDefault(t => t.Id == id);
            this.testsRepository.Delete(test);
            await this.testsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByProblemId<T>(int problemId)
        {
            return this.testsRepository
                .All()
                .Where(t => t.ProblemId == problemId)
                .OrderBy(t => t.Id)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.testsRepository
                .All()
                .Where(t => t.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditTestInputModel input)
        {
            Test test = this.testsRepository.All().FirstOrDefault(t => t.Id == input.Id);

            test.Input = input.TestInput;
            test.Output = input.ExpectedOutput;

            await this.testsRepository.SaveChangesAsync();
        }
    }
}
