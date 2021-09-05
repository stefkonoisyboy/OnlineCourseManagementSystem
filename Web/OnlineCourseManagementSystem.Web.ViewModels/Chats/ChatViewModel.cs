namespace OnlineCourseManagementSystem.Web.ViewModels.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class ChatViewModel : IMapFrom<ChatUser>, IHaveCustomMappings
    {
        [IgnoreMap]
        public string Name { get; set; }

        public int ChatId { get; set; }

        public string CreatorId { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }

        public bool IsGroupChat { get; set; }

        public string IconUrl { get; set; }

        [IgnoreMap]
        public int UsersPerChatCount => this.Users.ToList().Count;

        public bool IsMuted { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ChatUser, ChatViewModel>()
                .ForMember(x => x.IsGroupChat, y => y.MapFrom(cu => cu.Chat.ChatType == ChatType.GroupChat))
                .ForMember(x => x.IconUrl, y => y.MapFrom(cu => cu.Chat.IconRemoteUrl))
                .ForMember(x => x.CreatorId, y => y.MapFrom(cu => cu.Chat.CreatorId));
        }
    }
}
