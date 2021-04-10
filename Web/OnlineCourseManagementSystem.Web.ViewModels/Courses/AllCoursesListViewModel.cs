namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllCoursesListViewModel
    {
        public IEnumerable<AllCoursesViewModel> Courses { get; set; }
    }
}
