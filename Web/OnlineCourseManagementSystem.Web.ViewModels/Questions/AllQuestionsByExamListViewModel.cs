using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    public class AllQuestionsByExamListViewModel
    {
        public IEnumerable<AllQuestionsByExamViewModel> Questions { get; set; }
    }
}
