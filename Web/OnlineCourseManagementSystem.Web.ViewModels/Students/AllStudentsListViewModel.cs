namespace OnlineCourseManagementSystem.Web.ViewModels.Students
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllStudentsListViewModel
    {
        public IEnumerable<AllStudentsViewModel> Students { get; set; }
    }
}
