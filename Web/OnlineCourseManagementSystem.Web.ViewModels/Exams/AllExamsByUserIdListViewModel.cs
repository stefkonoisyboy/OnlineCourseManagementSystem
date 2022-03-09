namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class AllExamsByUserIdListViewModel : PagingViewModel
    {
        public IEnumerable<AllExamsByUserIdViewModel> Exams { get; set; }

        public string Input { get; set; }
    }
}
