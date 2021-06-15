namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllChatsViewModel
    {
        public IEnumerable<ChatViewModel> Chats { get; set; }
    }
}
