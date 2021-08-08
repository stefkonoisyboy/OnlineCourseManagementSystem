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

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
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
    }
}
