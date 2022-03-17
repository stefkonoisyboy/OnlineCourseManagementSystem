namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using Ganss.XSS;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Choices;

    public class AllQuestionsByExamViewModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        public string ExamName { get; set; }

        public string Text { get; set; }

        [IgnoreMap]
        public string SanitizedText => new HtmlSanitizer().Sanitize(this.Text);

        public int ExamId { get; set; }

        public int Points { get; set; }

        public IEnumerable<AllChoicesByIdViewModel> Choices { get; set; }
    }
}
