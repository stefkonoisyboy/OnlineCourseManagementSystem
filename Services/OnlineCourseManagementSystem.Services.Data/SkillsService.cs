namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class SkillsService : ISkillsService
    {
        private readonly IDeletableEntityRepository<Skill> skillsRepository;

        public SkillsService(IDeletableEntityRepository<Skill> skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }

        public IEnumerable<T> GetAllByCourseId<T>(int id)
        {
            return this.skillsRepository
                .All()
                .OrderByDescending(s => s.CreatedOn)
                .Where(s => s.CourseId == id)
                .To<T>()
                .ToList();
        }
    }
}
