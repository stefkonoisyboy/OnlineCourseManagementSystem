using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using OnlineCourseManagementSystem.Web.ViewModels.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    public class QuestionDetailsViewModel : IMapFrom<Question>
    {
        public string ExamName { get; set; }

        public int ExamId { get; set; }

        public string Text { get; set; }

        public IEnumerable<AllChoicesByIdViewModel> Choices { get; set; }
    }
}
