namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class EditChatInputModel
    {
        public int ChatId { get; set; }

        public IFormFile Icon { get; set; }

        public string Name { get; set; }
    }
}
