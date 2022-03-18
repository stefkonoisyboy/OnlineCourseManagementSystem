namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class AllCoursesBySubjectListViewModel : BasePagingViewModel
    {
        public int SubjectId { get; set; }

        public IEnumerable<AllCoursesBySubjectViewModel> Courses { get; set; }

        public override int ItemsPerPage => 4;
    }
}
