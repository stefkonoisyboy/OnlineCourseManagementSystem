using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Dashboard
{
    public class AdminDashboardViewModel
    {
        public int TotalStudents { get; set; }

        public int TotalTeachers { get; set; }

        public int TotalCourses { get; set; }

        public IEnumerable<Top4TeachersViewModel> Teachers { get; set; }

        public IEnumerable<Top4StudentsViewModel> Students { get; set; }
    }
}
