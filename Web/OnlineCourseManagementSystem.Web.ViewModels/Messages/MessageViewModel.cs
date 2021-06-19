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
        public string Content { get; set; }

        public string CreatorName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageViewModel>()
                .ForMember(x => x.CreatorName, y => y.MapFrom(m => $"{m.Creator.FirstName} {m.Creator.LastName}"));
        }
    }
}
