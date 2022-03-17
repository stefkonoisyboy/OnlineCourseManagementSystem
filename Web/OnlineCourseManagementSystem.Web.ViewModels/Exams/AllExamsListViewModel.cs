namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using OnlineCourseManagementSystem.Web.ViewModels.Paging;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllExamsListViewModel : PagingViewModel
    {
        public IEnumerable<AllExamsViewModel> Exams { get; set; }

        public string Input { get; set; }
    }
}
