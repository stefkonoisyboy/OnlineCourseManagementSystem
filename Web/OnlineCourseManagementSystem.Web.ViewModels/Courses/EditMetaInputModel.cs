namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class EditMetaInputModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [IgnoreMap]
        public IEnumerable<SelectListItem> SubjectsItems { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "Start Date should be higher than Current Date!")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        public int RecommendedDuration { get; set; }

        public int FileId { get; set; }

        public string FileRemoteUrl { get; set; }

        [IgnoreMap]
        [ImageExtensionValidationAttribute(ErrorMessage = "Invalid file format!")]
        public IFormFile Image { get; set; }

        [IgnoreMap]
        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
