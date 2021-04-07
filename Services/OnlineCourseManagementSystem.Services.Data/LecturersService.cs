namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class LecturersService : ILecturersService
    {
        private readonly IDeletableEntityRepository<Lecturer> lecturersRepository;

        public LecturersService(IDeletableEntityRepository<Lecturer> lecturersRepository)
        {
            this.lecturersRepository = lecturersRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.lecturersRepository
                .All()
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .To<T>()
                .ToList();
        }
    }
}
