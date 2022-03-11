using OnlineCourseManagementSystem.Data.Common.Repositories;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public class ExecutedTestsService : IExecutedTestsService
    {
        private readonly IDeletableEntityRepository<ExecutedTest> executedTestsRepository;

        public ExecutedTestsService(IDeletableEntityRepository<ExecutedTest> executedTestsRepository)
        {
            this.executedTestsRepository = executedTestsRepository;
        }

        public IEnumerable<T> GetAllBySubmission<T>(int submissionId)
        {
            return this.executedTestsRepository
                .All()
                .Where(et => et.SubmissionId == submissionId)
                .OrderBy(et => et.Id)
                .To<T>()
                .ToList();
        }
    }
}
