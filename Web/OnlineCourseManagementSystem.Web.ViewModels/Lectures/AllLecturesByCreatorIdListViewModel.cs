using OnlineCourseManagementSystem.Web.ViewModels.Courses;
using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using OnlineCourseManagementSystem.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    public class AllLecturesByCreatorIdListViewModel : PagingViewModel
    {
        public IEnumerable<AllLecturesByCreatorIdViewModel> Lectures { get; set; }

        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }

        public CreateLectureInputModel Input { get; set; }

        public EditLectureInputModel Edit { get; set; }
    }
}
