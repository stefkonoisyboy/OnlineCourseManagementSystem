using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    public class AllActiveCoursesListViewModel : PagingViewModel
    {
        public IEnumerable<AllActiveCoursesViewModel> ActiveCourses { get; set; }
    }
}
