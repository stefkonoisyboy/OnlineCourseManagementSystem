namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditCheckedAssignmentInputModel : IMapFrom<UserAssignment>
    {
        public int AssignmentId { get; set; }

        public string UserId { get; set; }

        public int Points { get; set; }

        public string Feedback { get; set; }
    }
}
