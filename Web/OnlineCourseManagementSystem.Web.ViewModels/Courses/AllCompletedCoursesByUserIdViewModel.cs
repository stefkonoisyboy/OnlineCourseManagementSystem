namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class AllCompletedCoursesByUserIdViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string FileRemoteUrl { get; set; }

        public string Name { get; set; }

        [IgnoreMap]
        public IEnumerable<AllTagsByCourseIdViewModel> Tags { get; set; }

        [IgnoreMap]
        public int CompletedLecturesCount { get; set; }

        public int LecturesCount { get; set; }
    }
}
