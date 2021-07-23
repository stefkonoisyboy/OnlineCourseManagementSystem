namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditChatInputModel : IMapFrom<Chat>
    {
        public int Id { get; set; }

        [IgnoreMap]
        public IFormFile Icon { get; set; }

        public string IconRemoteUrl { get; set; }

        public string Name { get; set; }
    }
}
