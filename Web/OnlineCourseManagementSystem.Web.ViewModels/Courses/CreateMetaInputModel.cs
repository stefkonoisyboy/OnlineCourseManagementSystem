namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class CreateMetaInputModel
    {
        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<SelectListItem> CoursesItems { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int SubjectId { get; set; }

        public IEnumerable<SelectListItem> SubjectsItems { get; set; }

        public IEnumerable<int> Tags { get; set; }

        public IEnumerable<SelectListItem> TagsItems { get; set; }

        public IEnumerable<string> Lecturers { get; set; }

        public IEnumerable<SelectListItem> LecturersItems { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "Start Date should be higher than Current Date!")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        public int RecommendedDuration { get; set; }

        [Required]
        [Display(Name = "Skills (list all skills, separated by a semi-colon)")]
        public string Skills { get; set; }

        [Required]
        [Display(Name = "Trailer (Video from YouTube)")]
        public string TrailerRemoteUrl { get; set; }

        [ImageExtensionValidationAttribute(ErrorMessage = "Invalid file format!")]
        public IFormFile Image { get; set; }

        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
