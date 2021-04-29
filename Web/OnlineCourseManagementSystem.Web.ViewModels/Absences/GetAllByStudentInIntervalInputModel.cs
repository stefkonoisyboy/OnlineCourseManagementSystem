namespace OnlineCourseManagementSystem.Web.ViewModels.Absences
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class GetAllByStudentInIntervalInputModel : IValidatableObject
    {
        public string StudentId { get; set; }

        public IEnumerable<SelectListItem> StudentItems { get; set; }

        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> CourseItems { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate > DateTime.UtcNow)
            {
                yield return new ValidationResult("Start Date cannot be greater than Current Date");
            }

            if (this.EndDate > DateTime.UtcNow)
            {
                yield return new ValidationResult("End Date cannot be greater than Current Date");
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start Date cannot be greater than End Date");
            }
        }
    }
}
