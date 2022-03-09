namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class CertificatesService : ICertificatesService
    {
        private readonly IDeletableEntityRepository<Certificate> certificatesRepository;

        public CertificatesService(IDeletableEntityRepository<Certificate> certificatesRepository)
        {
            this.certificatesRepository = certificatesRepository;
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId)
        {
            return this.certificatesRepository
                .All()
                .Where(c => c.UserId == userId)
                .To<T>()
                .ToList();
        }

        public T GetByUserIdAndCourseId<T>(string userId, int courseId)
        {
            return this.certificatesRepository
                .All()
                .Where(c => c.UserId == userId && c.CourseId == courseId)
                .To<T>()
                .FirstOrDefault();
        }
    }
}
