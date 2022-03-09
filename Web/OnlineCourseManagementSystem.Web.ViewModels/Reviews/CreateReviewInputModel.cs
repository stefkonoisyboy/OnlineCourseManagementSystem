namespace OnlineCourseManagementSystem.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateReviewInputModel
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public int CourseId { get; set; }

        public double Rating { get; set; }
    }
}
