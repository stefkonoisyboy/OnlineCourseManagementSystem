namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditChatInputModel : IMapFrom<ChatUser>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [IgnoreMap]
        public IFormFile Icon { get; set; }

        public string IconRemoteUrl { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ChatUser, EditChatInputModel>()
                .ForMember(x => x.Id, y => y.MapFrom(cu => cu.ChatId))
                .ForMember(x => x.IconRemoteUrl, y => y.MapFrom(cu => cu.Chat.IconRemoteUrl))
                .ForMember(x => x.Name, y => y.MapFrom(cu => cu.Chat.Name));
        }
    }
}
