namespace OnlineCourseManagementSystem.Web.ViewModels.MessageQAs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditMessageQAInputModel : IMapFrom<MessageQA>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(160)]
        public string Content { get; set; }
    }
}
