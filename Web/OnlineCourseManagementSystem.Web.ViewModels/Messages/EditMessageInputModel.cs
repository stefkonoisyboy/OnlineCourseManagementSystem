namespace OnlineCourseManagementSystem.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditMessageInputModel : IMapFrom<Message>
    {
        public int Id { get; set; } = 0;

        [Required]
        public string Content { get; set; }
    }
}
