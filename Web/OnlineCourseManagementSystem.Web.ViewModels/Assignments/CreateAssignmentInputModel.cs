namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class CreateAssignmentInputModel
    {
        public string Instructions { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public IEnumerable<string> StudentsId { get; set; }

        [Required]
        public IEnumerable<SelectListItem> Students { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [CheckIfDateIsGreaterThanCurrentDateValidation(ErrorMessage = "Start Date should be higher than Current Date!")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }

        public int PossiblePoints { get; set; }
    }
}
