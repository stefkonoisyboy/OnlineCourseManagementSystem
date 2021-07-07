using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    public class AllExamsByUserIdListViewModel
    {
        public IEnumerable<AllExamsByUserIdViewModel> Exams { get; set; }
    }
}
