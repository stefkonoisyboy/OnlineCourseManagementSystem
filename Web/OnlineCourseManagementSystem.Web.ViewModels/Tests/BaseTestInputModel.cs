namespace OnlineCourseManagementSystem.Web.ViewModels.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BaseTestInputModel
    {
        [Required]
        [MinLength(1)]
        public string TestInput { get; set; }

        [Required]
        [MinLength(1)]
        public string ExpectedOutput { get; set; }
    }
}
