namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class CreateAssignmentInputModel : BaseAssignmentInputModel
    {
        public IEnumerable<IFormFile> Files { get; set; }

        [Required]
        public IEnumerable<string> StudentsId { get; set; }

        public IEnumerable<SelectListItem> Students { get; set; }
    }
}
