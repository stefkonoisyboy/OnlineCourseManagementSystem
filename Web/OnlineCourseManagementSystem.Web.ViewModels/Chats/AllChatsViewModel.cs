namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class AllChatsViewModel
    {
        public IEnumerable<ChatViewModel> Chats { get; set; }

        public IEnumerable<ChatViewModel> PinnedChats { get; set; }
    }
}
