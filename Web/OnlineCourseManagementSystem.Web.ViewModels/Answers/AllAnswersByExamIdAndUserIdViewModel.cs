using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Answers
{
    public class AllAnswersByExamIdAndUserIdViewModel : IMapFrom<Answer>
    {
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public bool IsCorrect { get; set; }
    }
}
