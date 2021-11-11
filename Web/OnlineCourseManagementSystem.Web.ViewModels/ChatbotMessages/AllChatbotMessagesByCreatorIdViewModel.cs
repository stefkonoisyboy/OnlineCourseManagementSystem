using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages
{
    public class AllChatbotMessagesByCreatorIdViewModel : IMapFrom<ChatbotMessage>
    {
        public string Content { get; set; }

        public string CreatorId { get; set; }

        public bool IsMessageFromChatbot { get; set; }
    }
}
