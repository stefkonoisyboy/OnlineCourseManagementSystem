namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class CreateCourseInputModel : BaseCourseInputModel
    {
        public string UserId { get; set; }

        [ImageExtensionValidationAttribute(ErrorMessage = "Invalid file format!")]
        public IFormFile Image { get; set; }

        public IEnumerable<int> Tags { get; set; }

        public IEnumerable<SelectListItem> TagItems { get; set; }

        public IEnumerable<string> Lecturers { get; set; }

        public IEnumerable<SelectListItem> LecturerItems { get; set; }
    }
}
