namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllCoursesBySubjectAndCourseNameViewModel
    {
        public string CourseName { get; set; }

        public LastActiveCourseViewModel LastActiveCourse { get; set; }

        public IEnumerable<AllCoursesBySubjectViewModel> Courses { get; set; }
    }
}
