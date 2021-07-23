namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AddUsersToChatInputModel
    {
        public string UserId { get; set; }

        public int ChatId { get; set; }

        public IEnumerable<string> UsersId { get; set; }
    }
}
