namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllAssignmentsViewModel
    {
        public IEnumerable<AssignmentViewModel> UnfinishedAssignments { get; set; }

        public IEnumerable<FinishedAssignmentViewModel> FinishedAssignments { get; set; }
    }
}
