namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BaseContestInputModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(250)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
