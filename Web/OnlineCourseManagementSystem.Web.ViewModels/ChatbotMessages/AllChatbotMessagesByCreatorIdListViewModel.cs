using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages
{
    public class AllChatbotMessagesByCreatorIdListViewModel
    {
        public IEnumerable<AllChatbotMessagesByCreatorIdViewModel> Messages { get; set; }

        public CreateChatbotMessageInputModel Input { get; set; }
    }
}
