namespace OnlineCourseManagementSystem.Web.ViewModels.Admins
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Events;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Posts;

    public class AdminItemsViewModel
    {
        public IEnumerable<CourseByAdminViewModel> Courses { get; set; }

        public IEnumerable<EventByAdminViewModel> Events { get; set; }

        public IEnumerable<PostByAdminViewModel> Posts { get; set; }

        public IEnumerable<ExamByAdminViewModel> Exams { get; set; }

        public IEnumerable<LectureByAdminViewModel> Lectures { get; set; }

        public IEnumerable<AssignmentByAdminViewModel> Assignments { get; set; }
    }
}
