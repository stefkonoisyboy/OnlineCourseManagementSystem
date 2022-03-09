namespace OnlineCourseManagementSystem.Web.ViewModels.Submissions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateSubmissionInputModel
    {
        [Required]
        [MinLength(1)]
        public string Code { get; set; }

        public int ProblemId { get; set; }

        public int ContestId { get; set; }

        public string UserId { get; set; }
    }
}
