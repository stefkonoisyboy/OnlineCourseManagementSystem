namespace OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllChatbotMessagesByCreatorIdListViewModel
    {
        public IEnumerable<AllChatbotMessagesByCreatorIdViewModel> Messages { get; set; }

        public CreateChatbotMessageInputModel Input { get; set; }
    }
}
