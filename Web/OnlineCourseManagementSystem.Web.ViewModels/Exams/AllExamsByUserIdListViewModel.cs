using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    public class AllExamsByUserIdListViewModel : PagingViewModel
    {
        public IEnumerable<AllExamsByUserIdViewModel> Exams { get; set; }

        public string Input { get; set; }
    }
}
