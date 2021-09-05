namespace OnlineCourseManagementSystem.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string CreatorName { get; set; }

        public string CreatorProfileImageUrl { get; set; }

        public string CreatorId { get; set; }

        public bool IsModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageViewModel>()
                .ForMember(x => x.CreatorName, y => y.MapFrom(m => $"{m.Creator.FirstName} {m.Creator.LastName}"))
                .ForMember(x => x.IsModified, y => y.MapFrom(m => m.ModifiedOn != null))
                .ForMember(x => x.CreatorProfileImageUrl, y => y.MapFrom(m => m.Creator.ProfileImageUrl));
        }
    }
}
