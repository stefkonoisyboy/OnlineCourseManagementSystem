namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class LectureByAdminViewModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CreatorId { get; set; }

        public UserViewModel Creator { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CourseViewModel Course { get; set; }

        public bool IsCompleted { get; set; }
    }
}
