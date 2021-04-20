namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class BaseAssignmentInputModel
    {
        [MinLength(5)]
        public string Instructions { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int PossiblePoints { get; set; }
    }
}
