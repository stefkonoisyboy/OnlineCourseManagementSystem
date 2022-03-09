namespace OnlineCourseManagementSystem.Web.ViewModels.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class Top3StudentsByCompletedAssignmentsViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AssignmentsCount { get; set; }
    }
}
