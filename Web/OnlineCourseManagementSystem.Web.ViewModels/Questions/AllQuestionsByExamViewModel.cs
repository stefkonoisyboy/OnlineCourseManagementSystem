using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    public class AllQuestionsByExamViewModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        public string ExamName { get; set; }

        public string Text { get; set; }

        public int ExamId { get; set; }
    }
}
