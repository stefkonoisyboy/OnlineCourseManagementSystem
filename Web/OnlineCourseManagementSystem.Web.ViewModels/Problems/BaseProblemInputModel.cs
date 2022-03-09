namespace OnlineCourseManagementSystem.Web.ViewModels.Problems
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BaseProblemInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Points { get; set; }

        public int ContestId { get; set; }
    }
}
