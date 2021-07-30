namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class BaseAssignmentInputModel : IValidatableObject
    {
        [Required]
        public string Title { get; set; }

        [MinLength(10, ErrorMessage = "Instructions need be at least 10 symbols")]
        public string Instructions { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "Start Date should be higher than Current Date!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "End Date should be higher than Current Date!")]
        public DateTime EndDate { get; set; }

        public int PossiblePoints { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start Date need to be lower than the End Date");
            }
        }
    }
}
