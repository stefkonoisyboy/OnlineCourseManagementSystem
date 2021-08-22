using AutoMapper;
using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using OnlineCourseManagementSystem.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    public class AllCoursesByCurrentLecturerListViewModel : PagingViewModel
    {
        public IEnumerable<AllCoursesByCurrentLecturerViewModel> Courses { get; set; }

        [IgnoreMap]
        public IEnumerable<AllRecommendedCoursesByIdViewModel> FeaturedCourses { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
