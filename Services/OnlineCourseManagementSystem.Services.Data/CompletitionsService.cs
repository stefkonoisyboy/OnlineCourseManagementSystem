namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;

    public class CompletitionsService : ICompletitionsService
    {
        private readonly IDeletableEntityRepository<Completition> completitionsRepository;

        public CompletitionsService(IDeletableEntityRepository<Completition> completitionsRepository)
        {
            this.completitionsRepository = completitionsRepository;
        }

        public int GetAllCompletitionsCountByCourseIdAndUserId(int courseId, string userId)
        {
            return this.completitionsRepository
                .All()
                .Count(c => c.Lecture.CourseId == courseId && c.UserId == userId);
        }
    }
}
