namespace OnlineCourseManagementSystem.Web.ViewModels.Certificates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class CertificateByUserIdAndCourseIdViewModel : IMapFrom<Certificate>
    {
        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string CourseName { get; set; }

        public DateTime CourseCreatedOn { get; set; }

        public IEnumerable<AllLecturesByIdViewModel> Lectures { get; set; }

        [IgnoreMap]
        public double Grade { get; set; }

        [IgnoreMap]
        public IEnumerable<AllRecommendedCoursesByIdViewModel> FeaturedCourses { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
