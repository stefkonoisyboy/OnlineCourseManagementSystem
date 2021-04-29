namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllCoursesByUserViewModel : IMapFrom<UserCourse>
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public decimal CoursePrice { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }

        public string CourseFileRemoteUrl { get; set; }

        public string CourseSubjectName { get; set; }
    }
}
