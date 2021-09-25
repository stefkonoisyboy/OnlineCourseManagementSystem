using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourseManagementSystem.Web.ViewModels.Courses;
using OnlineCourseManagementSystem.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    public class AddExamToLectureInputModel
    {
        [Display(Name = "Exam")]
        public int ExamId { get; set; }

        public IEnumerable<SelectListItem> Exams { get; set; }

        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
