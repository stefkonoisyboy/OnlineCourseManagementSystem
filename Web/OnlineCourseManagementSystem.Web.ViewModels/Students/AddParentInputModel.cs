namespace OnlineCourseManagementSystem.Web.ViewModels.Students
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AddParentInputModel
    {
        [Display(Name = "Student")]
        public string StudentId { get; set; }

        public IEnumerable<SelectListItem> Students { get; set; }

        [Display(Name = "Parent")]
        public string ParentId { get; set; }

        public IEnumerable<SelectListItem> Parents { get; set; }
    }
}
