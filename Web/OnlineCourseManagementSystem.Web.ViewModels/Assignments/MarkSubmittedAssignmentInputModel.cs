namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class MarkSubmittedAssignmentInputModel
    {
        public int AssignmentId { get; set; }

        public string UserId { get; set; }

        public int Points { get; set; }

        public string Feedback { get; set; }
    }
}
