namespace OnlineCourseManagementSystem.Web.ViewModels.Choices
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class CreateChoiceInputModel : IMapFrom<Choice>
    {
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public string Text { get; set; }
    }
}
