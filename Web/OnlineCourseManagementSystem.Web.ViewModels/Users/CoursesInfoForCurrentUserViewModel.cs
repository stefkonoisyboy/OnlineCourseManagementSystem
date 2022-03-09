namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Certificates;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;

    public class CoursesInfoForCurrentUserViewModel
    {
        public IEnumerable<AllCertificatesByUserIdViewModel> Certificates { get; set; }

        public IEnumerable<AllCompletedCoursesByUserIdViewModel> CompletedCourses { get; set; }

        public IEnumerable<AllFollowedCoursesByUserIdViewModel> FollowedCourses { get; set; }

        public IEnumerable<AllRecommendedCoursesByIdViewModel> FeaturedCourses { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
