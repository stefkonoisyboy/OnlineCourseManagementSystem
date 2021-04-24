using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    public class MarkSubmittedAssignmentInputModel
    {
        public int AssignmentId { get; set; }

        public string UserId { get; set; }

        [Required]
        public int Points { get; set; }

        public string Feedback { get; set; }
    }
}
