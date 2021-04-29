namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllCoursesByUserListViewModel
    {
        public IEnumerable<AllCoursesByUserViewModel> Courses { get; set; }
    }
}
