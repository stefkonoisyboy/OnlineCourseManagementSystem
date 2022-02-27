namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllLectureAssignmentsViewModel
    {
        public IEnumerable<LectureAssignmentViewModel> CreatedAssignments { get; set; }
    }
}
