namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ChatViewModel : IMapFrom<ChatUser>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public int ChatId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ChatUser, ChatViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(cu => cu.Chat.Name));
        }
    }
}
