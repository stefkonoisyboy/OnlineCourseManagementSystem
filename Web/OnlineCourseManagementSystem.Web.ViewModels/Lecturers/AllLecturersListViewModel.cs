using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lecturers
{
    public class AllLecturersListViewModel
    {
        public IEnumerable<AllLecturersViewModel> Lecturers { get; set; }
    }
}
