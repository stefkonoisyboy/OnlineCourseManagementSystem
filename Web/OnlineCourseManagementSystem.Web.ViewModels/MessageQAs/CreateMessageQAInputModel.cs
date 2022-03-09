namespace OnlineCourseManagementSystem.Web.ViewModels.MessageQAs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateMessageQAInputModel
    {

        [Required]
        [MaxLength(160)]
        public string Content { get; set; }

        public string CreatorId { get; set; }

        public int ChannelId { get; set; }
    }
}
