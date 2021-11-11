using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    public class ResultFromExamListViewModel : PagingViewModel
    {
        public IEnumerable<ResultFromExamViewModel> Exams { get; set; }

        public string Input { get; set; }
    }
}
