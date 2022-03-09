namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class AllActiveCoursesListViewModel : PagingViewModel
    {
        public IEnumerable<AllActiveCoursesViewModel> ActiveCourses { get; set; }
    }
}
