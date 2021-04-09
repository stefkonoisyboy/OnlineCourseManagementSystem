namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class CreateCourseInputModel : BaseCourseInputModel, IValidatableObject
    {
        public string UserId { get; set; }

        [ImageExtensionValidationAttribute(ErrorMessage = "Invalid file format!")]
        public IFormFile Image { get; set; }

        public IEnumerable<int> Tags { get; set; }

        public IEnumerable<SelectListItem> TagItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("Start Date should be higher than Current Date!");
            }

            if (this.EndDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("End Date should be higher than Current Date!");
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start Date should be lower than End Date!");
            }
        }
    }
}
