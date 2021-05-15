using OnlineCourseManagementSystem.Data.Common.Repositories;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public class ChoicesService : IChoicesService
    {
        private readonly IDeletableEntityRepository<Choice> choicesRepository;

        public ChoicesService(IDeletableEntityRepository<Choice> choicesRepository)
        {
            this.choicesRepository = choicesRepository;
        }

        public IEnumerable<T> GetAllById<T>(int id)
        {
            return this.choicesRepository
                .All()
                .Where(c => c.QuestionId == id)
                .To<T>()
                .ToList();
        }
    }
}
