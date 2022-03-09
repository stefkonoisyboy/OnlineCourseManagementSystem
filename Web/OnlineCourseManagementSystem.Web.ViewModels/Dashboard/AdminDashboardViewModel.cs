namespace OnlineCourseManagementSystem.Web.ViewModels.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AdminDashboardViewModel
    {
        public int TotalStudents { get; set; }

        public int TotalTeachers { get; set; }

        public int TotalCourses { get; set; }

        public IEnumerable<Top4TeachersViewModel> Teachers { get; set; }

        public IEnumerable<Top4StudentsViewModel> Students { get; set; }
    }
}
