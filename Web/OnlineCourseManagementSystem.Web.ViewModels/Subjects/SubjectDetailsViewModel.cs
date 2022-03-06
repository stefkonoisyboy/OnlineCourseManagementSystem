namespace OnlineCourseManagementSystem.Web.ViewModels.Subjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;

    public class SubjectDetailsViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [IgnoreMap]
        public IEnumerable<AllCoursesBySubjectViewModel> CurrentYearCourses { get; set; }

        [IgnoreMap]
        public AllCoursesBySubjectListViewModel CoursesBySubjectListViewModel { get; set; }
    }
}
