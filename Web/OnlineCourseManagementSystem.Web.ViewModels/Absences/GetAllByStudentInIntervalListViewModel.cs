using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Absences
{
    public class GetAllByStudentInIntervalListViewModel
    {
        public string StudentFullName { get; set; }

        public string CourseName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<GetAllByStudentInIntervalViewModel> Students { get; set; }
    }
}
