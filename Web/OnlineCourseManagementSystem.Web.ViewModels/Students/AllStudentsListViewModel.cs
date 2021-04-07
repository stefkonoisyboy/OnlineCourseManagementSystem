using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Students
{
    public class AllStudentsListViewModel
    {
        public IEnumerable<AllStudentsViewModel> Students { get; set; }
    }
}
