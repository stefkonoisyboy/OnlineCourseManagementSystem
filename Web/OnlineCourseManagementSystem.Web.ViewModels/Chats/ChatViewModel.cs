namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class ChatViewModel : IMapFrom<ChatUser>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public int ChatId { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }

        [IgnoreMap]
        public int UsersPerChatCount => this.Users.ToList().Count;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ChatUser, ChatViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(cu => cu.Chat.Name));
        }
    }
}
