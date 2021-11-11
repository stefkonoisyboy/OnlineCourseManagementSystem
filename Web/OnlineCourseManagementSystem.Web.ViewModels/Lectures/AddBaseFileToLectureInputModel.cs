using OnlineCourseManagementSystem.Web.ViewModels.Courses;
using OnlineCourseManagementSystem.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    public class AddBaseFileToLectureInputModel
    {
        public string UserId { get; set; }

        public int LectureId { get; set; }

        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
