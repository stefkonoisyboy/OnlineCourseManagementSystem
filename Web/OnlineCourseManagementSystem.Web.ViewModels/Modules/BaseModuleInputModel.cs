namespace OnlineCourseManagementSystem.Web.ViewModels.Modules
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BaseModuleInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> SubjectItems { get; set; }

        [Required]
        public IEnumerable<int> SubjectIds { get; set; }
    }
}
