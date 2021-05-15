namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllExamsListViewModel
    {
        public IEnumerable<AllExamsViewModel> Exams { get; set; }
    }
}
