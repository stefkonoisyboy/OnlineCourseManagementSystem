namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class ExamByAdminViewModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LectureId { get; set; }

        public UserViewModel Lecturer { get; set; }

        public CourseViewModel Course { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PassMarks { get; set; }

        public int Duration { get; set; }

        public bool IsActive { get; set; }

        public int CountOfUsers { get; set; }
    }
}
