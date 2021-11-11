namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class UserExam : BaseDeletableModel<int>
    {
        public int ExamId { get; set; }

        public virtual Exam Exam { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public double Grade { get; set; }

        public Status Status { get; set; }

        public DateTime? SeenOn { get; set; }

        public DateTime FinishedOn { get; set; }
    }
}
