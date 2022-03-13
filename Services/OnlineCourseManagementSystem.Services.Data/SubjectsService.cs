namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Subjects;

    public class SubjectsService : ISubjectsService
    {
        private readonly IDeletableEntityRepository<Subject> subjectsRepository;

        public SubjectsService(IDeletableEntityRepository<Subject> subjectsRepository)
        {
            this.subjectsRepository = subjectsRepository;
        }

        public async Task CreateAsync(SubjectInputModel inputModel)
        {
            Subject subject = new Subject()
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
            };

            await this.subjectsRepository.AddAsync(subject);
            await this.subjectsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.subjectsRepository
                .All()
                .OrderBy(s => s.Name)
                .To<T>()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.subjectsRepository
                .All()
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                })
                .ToList();
        }

        public IEnumerable<T> GetAllByModule<T>(int moduleId)
        {
            return this.subjectsRepository
                .All()
                .Where(s => s.ModuleId == moduleId)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllWithReferedCsharpCourses<T>()
        {
            return this.subjectsRepository
                    .All()
                    .Where(s => s.Name.Contains("C#"))
                    .Include(s => s.Courses)
                    .To<T>()
                    .ToList();
        }

        public IEnumerable<T> GetAllWithReferedJavaCourses<T>()
        {
            return this.subjectsRepository
                .All()
                .Where(s => s.Name.Contains("Java"))
                .Include(s => s.Courses)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllWithReferedJSCourses<T>()
        {
            return this.subjectsRepository
                .All()
                .Where(s => s.Name.Contains("JS"))
                .Include(s => s.Courses)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.subjectsRepository
                .All()
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetByIdAndCourseName<T>(int subjectId, string courseName)
        {
            return this.subjectsRepository
                .All()
                .Where(s => s.Id == subjectId && s.Courses.Any(c => c.Name.Contains(courseName)))
                .To<T>()
                .ToList();
        }

        public T GetByName<T>(string name)
        {
            return this.subjectsRepository
                .All()
                .Where(s => s.Name.Contains(name))
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditSubjectInputModel inputModel)
        {
            Subject subject = this.subjectsRepository.All().FirstOrDefault(s => s.Id == inputModel.Id);
            subject.Name = inputModel.Name;
            subject.Description = new HtmlSanitizer().Sanitize(inputModel.Description);

            await this.subjectsRepository.SaveChangesAsync();
        }
    }
}
