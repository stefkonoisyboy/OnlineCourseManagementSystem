namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class EditLectureInputModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [IgnoreMap]
        public IEnumerable<SelectListItem> CoursesItems { get; set; }

        [IgnoreMap]
        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
