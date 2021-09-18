namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Subjects;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class CourseByAdminViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public UserViewModel Creator { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int RecommendedDuration { get; set; }

        public SubjectViewModel Subject { get; set; }

        public string CourseLevel { get; set; }

        public bool IsApproved { get; set; }
    }
}
