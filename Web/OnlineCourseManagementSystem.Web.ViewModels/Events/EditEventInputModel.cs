namespace OnlineCourseManagementSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;

    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class EditEventInputModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        [Required]
        public string Theme { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "Start Date should be higher than Current Date!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "End Date should be higher than Current Date!")]
        public DateTime EndDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Instructions need be at least 10 symbols")]
        public string Description { get; set; }

        [IgnoreMap]
        public IEnumerable<IFormFile> FilesToAdd { get; set; }

        public IEnumerable<FileViewModel> Files { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start Date should be lower than End Date!");
            }
        }
    }
}
