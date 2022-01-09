namespace OnlineCourseManagementSystem.Web.ViewModels.Albums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BaseInputModel
    {
        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
