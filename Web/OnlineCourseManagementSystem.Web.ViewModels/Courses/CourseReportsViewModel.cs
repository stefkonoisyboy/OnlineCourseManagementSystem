namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Dashboard;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;

    public class CourseReportsViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public int LecturesCount { get; set; }

        public int AssignmentsCount { get; set; }

        public int ExamsCount { get; set; }

        public int PostsCount { get; set; }

        [IgnoreMap]
        public IEnumerable<AllLecturesForReportByCourseIdViewModel> Lectures { get; set; }

        [IgnoreMap]
        public IEnumerable<Top3StudentsByCompletedAssignmentsViewModel> Students { get; set; }
    }
}
