namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllQuestionsByExamListViewModel
    {
        public IEnumerable<AllQuestionsByExamViewModel> Questions { get; set; }
    }
}
