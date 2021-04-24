using OnlineCourseManagementSystem.Web.ViewModels.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    public class MarkSubmittedAssignmentViewModel
    {
        public IEnumerable<FileAssignmentViewModel> Files { get; set; }

        public MarkSubmittedAssignmentInputModel InputModel { get; set; }

        public string UserId { get; set; }
    }
}
