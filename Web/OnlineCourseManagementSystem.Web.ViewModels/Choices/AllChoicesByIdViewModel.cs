using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Choices
{
    public class AllChoicesByIdViewModel : IMapFrom<Choice>
    {
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
