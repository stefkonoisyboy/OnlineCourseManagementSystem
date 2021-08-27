using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Reviews
{
    public class CreateReviewInputModel
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public int CourseId { get; set; }

        public double Rating { get; set; }
    }
}
