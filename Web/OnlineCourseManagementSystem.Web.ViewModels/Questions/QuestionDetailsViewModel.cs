namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Choices;

    public class QuestionDetailsViewModel : IMapFrom<Question>
    {
        public string ExamName { get; set; }

        public int ExamId { get; set; }

        public string Text { get; set; }

        public IEnumerable<AllChoicesByIdViewModel> Choices { get; set; }
    }
}
