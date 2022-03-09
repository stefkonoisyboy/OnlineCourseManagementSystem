namespace OnlineCourseManagementSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Choices;

    public class CreateQuestionInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Question Name")]
        public string Text { get; set; }

        [Required]
        [Range(1, 6)]
        public int Points { get; set; }

        public int ExamId { get; set; }

        public string CorrectAnswerOption { get; set; }

        public List<CreateChoiceInputModel> Choices { get; set; }
    }
}
