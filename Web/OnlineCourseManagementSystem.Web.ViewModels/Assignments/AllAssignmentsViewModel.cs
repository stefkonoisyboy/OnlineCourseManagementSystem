using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    public class AllAssignmentsViewModel
    {
        public IEnumerable<AssignmentViewModel> UnFinihedAssignements { get; set; }

        public IEnumerable<FinishedAssignmentViewModel> FinishedAssignments { get; set; }
    }
}
