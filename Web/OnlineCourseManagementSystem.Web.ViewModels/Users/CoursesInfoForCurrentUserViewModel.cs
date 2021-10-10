using OnlineCourseManagementSystem.Web.ViewModels.Certificates;
using OnlineCourseManagementSystem.Web.ViewModels.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    public class CoursesInfoForCurrentUserViewModel
    {
        public IEnumerable<AllCertificatesByUserIdViewModel> Certificates { get; set; }

        public IEnumerable<AllCompletedCoursesByUserIdViewModel> CompletedCourses { get; set; }

        public IEnumerable<AllFollowedCoursesByUserIdViewModel> FollowedCourses { get; set; }

        public IEnumerable<AllRecommendedCoursesByIdViewModel> FeaturedCourses { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
