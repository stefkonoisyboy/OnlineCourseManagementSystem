namespace OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateChatbotMessageInputModel
    {
        public string Content { get; set; }

        public string CreatorId { get; set; }

        public bool IsMessageFromChatbot { get; set; }
    }
}
