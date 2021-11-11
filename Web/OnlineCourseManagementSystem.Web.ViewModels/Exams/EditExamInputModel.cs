namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class EditExamInputModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public int CourseId { get; set; }

        [Required]
        public int Duration { get; set; }

        public IEnumerable<SelectListItem> CourseItems { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation]
        public DateTime StartDate { get; set; }

        [IgnoreMap]
        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
