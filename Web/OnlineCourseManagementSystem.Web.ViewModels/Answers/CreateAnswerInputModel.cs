using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Answers
{
    public class CreateAnswerInputModel
    {
        public int QuestionId { get; set; }

        public bool IsCorrect { get; set; }

        public string Text { get; set; }
    }
}
