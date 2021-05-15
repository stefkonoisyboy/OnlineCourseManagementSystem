using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Choices
{
    public class CreateChoiceInputModel : IMapFrom<Choice>
    {
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public string Text { get; set; }
    }
}
