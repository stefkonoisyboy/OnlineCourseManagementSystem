namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class EditCourseInputModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [IgnoreMap]
        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
