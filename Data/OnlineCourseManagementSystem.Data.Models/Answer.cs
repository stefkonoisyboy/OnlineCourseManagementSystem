namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Answer : BaseDeletableModel<int>
    {
        public bool IsCorrect { get; set; }

        public string Text { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
