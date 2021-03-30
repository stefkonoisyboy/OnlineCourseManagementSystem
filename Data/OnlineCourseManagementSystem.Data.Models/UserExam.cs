namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class UserExam : BaseDeletableModel<int>
    {
        public int ExamId { get; set; }

        public virtual Exam Exam { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public double Grade { get; set; }
    }
}
