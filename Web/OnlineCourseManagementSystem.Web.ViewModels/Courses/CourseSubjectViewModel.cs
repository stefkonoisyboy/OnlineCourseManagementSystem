namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class CourseSubjectViewModel : IMapFrom<Course>
    {
        public string Name { get; set; }

        public int SubjectId { get; set; }
    }
}
