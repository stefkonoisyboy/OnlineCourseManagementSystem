namespace OnlineCourseManagementSystem.Web.ViewModels.Choices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllChoicesByIdViewModel : IMapFrom<Choice>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
