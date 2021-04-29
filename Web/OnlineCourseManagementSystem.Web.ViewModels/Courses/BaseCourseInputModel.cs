namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class BaseCourseInputModel : IValidatableObject
    {
        public BaseCourseInputModel()
        {
            this.SubjectsItems = new HashSet<SelectListItem>();
        }

        [Required]
        [MinLength(10)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public IEnumerable<SelectListItem> SubjectsItems { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "Start Date should be higher than Current Date!")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "End Date should be higher than Current Date!")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start Date should be lower than End Date!");
            }
        }
    }
}
