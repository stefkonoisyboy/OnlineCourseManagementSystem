namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllLectureAssignmetsByCourseViewModel : IMapFrom<Assignment>
    {
        public IEnumerable<LectureAssignmentViewModel> CreatedAssignments { get; set; }

        // public IEnumerable<LectureAssignmentViewModel> CheckedAssignmets { get; set; }

        public int CourseId { get; set; }

        // public string Category { get; set; }
    }
}
