namespace OnlineCourseManagementSystem.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateMessageInputModel
    {
        public string UserId { get; set; }

        public int? ChatId { get; set; }

        public string Content { get; set; }
    }
}
