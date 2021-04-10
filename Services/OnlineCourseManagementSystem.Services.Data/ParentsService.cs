namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ParentsService : IParentsService
    {
        private readonly IDeletableEntityRepository<Parent> parentsRepository;

        public ParentsService(IDeletableEntityRepository<Parent> parentsRepository)
        {
            this.parentsRepository = parentsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.parentsRepository
                .All()
                .OrderBy(p => p.User.FirstName + ' ' + p.User.LastName)
                .ThenBy(p => p.User.UserName)
                .Where(p => p.User.Roles.FirstOrDefault().RoleId.EndsWith("Parent"))
                .To<T>()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.parentsRepository
                .All()
                .OrderBy(p => p.User.FirstName + ' ' + p.User.LastName)
                .ThenBy(p => p.User.UserName)
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Parent"))
                .Select(p => new SelectListItem
                {
                    Text = p.User.FirstName + ' ' + p.User.LastName,
                    Value = p.Id,
                })
                .ToList();
        }
    }
}
