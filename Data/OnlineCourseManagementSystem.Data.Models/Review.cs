namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public double Rating { get; set; }
    }
}
