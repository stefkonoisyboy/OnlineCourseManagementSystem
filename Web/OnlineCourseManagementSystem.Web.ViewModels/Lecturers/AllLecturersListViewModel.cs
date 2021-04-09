namespace OnlineCourseManagementSystem.Web.ViewModels.Lecturers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllLecturersListViewModel
    {
        public IEnumerable<AllLecturersViewModel> Lecturers { get; set; }
    }
}
