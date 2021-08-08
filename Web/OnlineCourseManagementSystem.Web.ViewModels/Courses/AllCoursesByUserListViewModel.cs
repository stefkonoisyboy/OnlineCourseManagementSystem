namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using AutoMapper;
    using OnlineCourseManagementSystem.Web.ViewModels.Paging;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllCoursesByUserListViewModel : PagingViewModel
    {
        public IEnumerable<AllCoursesByUserViewModel> Courses { get; set; }

        [IgnoreMap]
        public IEnumerable<AllRecommendedCoursesByIdViewModel> FeaturedCourses { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
