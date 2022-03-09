namespace OnlineCourseManagementSystem.Web.ViewModels.Answers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateAnswerInputModel
    {
        public int QuestionId { get; set; }

        public bool IsCorrect { get; set; }

        public string Text { get; set; }
    }
}
