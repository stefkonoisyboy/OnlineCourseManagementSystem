namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    using OnlineCourseManagementSystem.Web.ViewModels.Paging;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllQuestionsByExamListViewModel : PagingViewModel
    {
        public IEnumerable<AllQuestionsByExamViewModel> Questions { get; set; }

        public string Input { get; set; }
    }
}
