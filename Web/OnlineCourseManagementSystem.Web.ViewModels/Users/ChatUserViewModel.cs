namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ChatUserViewModel : IMapFrom<ChatUser>
    {
        public string UserId { get; set; }

        [IgnoreMap]
        public string UserFullName { get; set; }

        public bool IsMuted { get; set; }
    }
}
