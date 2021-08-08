namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class LecturersService : ILecturersService
    {
        private readonly IDeletableEntityRepository<Lecturer> lecturersRepository;
        private readonly IDeletableEntityRepository<CourseLecturer> courseLecturersRepository;

        public LecturersService(IDeletableEntityRepository<Lecturer> lecturersRepository, IDeletableEntityRepository<CourseLecturer> courseLecturersRepository)
        {
            this.lecturersRepository = lecturersRepository;
            this.courseLecturersRepository = courseLecturersRepository;
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

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.lecturersRepository
                .All()
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .Select(l => new SelectListItem
                {
                    Text = l.User.FirstName + ' ' + l.User.LastName,
                    Value = l.Id,
                })
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAllByCourseAsSelectListItems(int courseId)
        {
            return this.lecturersRepository
               .All()
               .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
               .ThenBy(l => l.User.UserName)
               .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer") && l.Courses.Any(c => c.Id == courseId))
               .Select(l => new SelectListItem
               {
                   Text = l.User.FirstName + ' ' + l.User.LastName,
                   Value = l.Id,
               })
               .ToList();
        }

        public IEnumerable<T> GetAllByCourseId<T>(int courseId)
        {
            return this.courseLecturersRepository
                .All()
                .OrderBy(l => l.Lecturer.User.FirstName + ' ' + l.Lecturer.User.LastName)
                .ThenBy(l => l.Lecturer.User.UserName)
                .Where(l => l.CourseId == courseId && l.Lecturer.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllById<T>(int courseId)
        {
            return this.lecturersRepository
                .All()
                .Where(l => l.Courses.Any(c => c.Id == courseId) && l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .To<T>()
                .ToList();
        }
    }
}
