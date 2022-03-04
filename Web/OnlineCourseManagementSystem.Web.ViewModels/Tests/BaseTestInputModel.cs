using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Tests
{
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
