namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Course> courseRepository)
        {
            this.usersRepository = usersRepository;
            this.courseRepository = courseRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.usersRepository
                .All()
                .OrderBy(u => u.FirstName + ' ' + u.LastName)
                .ThenBy(u => u.UserName)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(string id)
        {
            return this.usersRepository
                .All()
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        //public T GetCourseCreatorById<T>(int courseId)
        //{
        //    var creator = this.courseRepository.All().FirstOrDefault(c => c.Id == courseId).Creator;
        //    return this.
        //}

        public string GetFullNameById(string id)
        {
            return this.usersRepository
                .All()
                .FirstOrDefault(u => u.Id == id)
                .FirstName + ' ' + this.usersRepository
                .All()
                .FirstOrDefault(u => u.Id == id)
                .LastName;
        }

        public string GetProfileImageUrlById(string id)
        {
            return this.usersRepository
               .All()
               .FirstOrDefault(u => u.Id == id)
               .ProfileImageUrl;
        }

    }
}
