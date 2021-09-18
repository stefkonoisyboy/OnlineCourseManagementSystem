namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;

    public class AssignmentByAdminViewModel : IMapFrom<Assignment>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public CourseViewModel Course { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PossiblePoints { get; set; }
    }
}
