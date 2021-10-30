namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Question : BaseDeletableModel<int>
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.Choices = new HashSet<Choice>();
        }

        public int ExamId { get; set; }

        public virtual Exam Exam { get; set; }

        public string Text { get; set; }

        public int Points { get; set; }

        public string CorrectAnswerOption { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Choice> Choices { get; set; }
    }
}
