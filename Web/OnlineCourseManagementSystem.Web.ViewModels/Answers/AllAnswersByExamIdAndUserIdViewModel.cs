namespace OnlineCourseManagementSystem.Web.ViewModels.Answers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllAnswersByExamIdAndUserIdViewModel : IMapFrom<Answer>
    {
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public bool IsCorrect { get; set; }
    }
}
