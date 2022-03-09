namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class AddExamToLectureInputModel
    {
        [Display(Name = "Exam")]
        public int ExamId { get; set; }

        public IEnumerable<SelectListItem> Exams { get; set; }

        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
