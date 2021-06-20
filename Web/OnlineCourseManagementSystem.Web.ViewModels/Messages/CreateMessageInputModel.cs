namespace OnlineCourseManagementSystem.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateMessageInputModel
    {
        public string UserId { get; set; }

        public int? ChatId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
