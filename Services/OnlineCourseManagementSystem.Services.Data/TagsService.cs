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

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<Tag> tagsRepository;
        private readonly IDeletableEntityRepository<CourseTag> courseTagsRepository;

        public TagsService(IDeletableEntityRepository<Tag> tagsRepository, IDeletableEntityRepository<CourseTag> courseTagsRepository)
        {
            this.tagsRepository = tagsRepository;
            this.courseTagsRepository = courseTagsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.tagsRepository
                .All()
                .OrderBy(t => t.Name)
                .To<T>()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.tagsRepository
                .All()
                .OrderBy(t => t.Name)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name,
                })
                .ToList();
        }

        public IEnumerable<T> GetAllByCourseId<T>(int courseId)
        {
            return this.courseTagsRepository
                .All()
                .OrderBy(t => t.Tag.Name)
                .Where(t => t.CourseId == courseId)
                .To<T>()
                .ToList();
        }
    }
}
