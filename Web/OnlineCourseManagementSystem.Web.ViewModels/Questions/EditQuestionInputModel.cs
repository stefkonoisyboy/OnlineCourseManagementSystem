namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Choices;

    public class EditQuestionInputModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Question Name")]
        public string Text { get; set; }

        [Required]
        [Range(1, 6)]
        public int Points { get; set; }

        public string CorrectAnswerOption { get; set; }

        [IgnoreMap]
        public List<CreateChoiceInputModel> Choices { get; set; }
    }
}
