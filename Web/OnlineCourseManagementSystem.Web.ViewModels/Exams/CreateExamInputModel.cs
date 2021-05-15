namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class CreateExamInputModel : IValidatableObject
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> CourseItems { get; set; }

        public string LecturerId { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation]
        public DateTime StartDate { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start Date cannot be greater than End Date");
            }
        }
    }
}
