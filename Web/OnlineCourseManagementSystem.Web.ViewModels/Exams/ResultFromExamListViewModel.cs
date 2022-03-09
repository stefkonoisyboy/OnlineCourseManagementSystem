namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class ResultFromExamListViewModel : PagingViewModel
    {
        public IEnumerable<ResultFromExamViewModel> Exams { get; set; }

        public string Input { get; set; }
    }
}
