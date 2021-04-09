namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class BaseCourseInputModel
    {
        [Required]
        [MinLength(10)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public IEnumerable<SelectListItem> SubjectItems { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
