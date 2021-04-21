using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    public class AllCoursesByUserListViewModel
    {
        public IEnumerable<AllCoursesByUserViewModel> Courses { get; set; }
    }
}
