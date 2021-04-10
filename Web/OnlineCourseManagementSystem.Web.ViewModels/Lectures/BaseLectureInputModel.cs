namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BaseLectureInputModel : IValidatableObject
    {
        [Required]
        [MinLength(10)]
        public string Title { get; set; }

        [CheckIfDateIsGreaterThanCurrentDateValidation]
        public DateTime StartDate { get; set; }

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
