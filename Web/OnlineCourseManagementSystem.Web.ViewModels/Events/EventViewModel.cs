namespace OnlineCourseManagementSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Theme { get; set; }

        public string CreatorFirstName { get; set; }

        public string CreatorLastName { get; set; }

        public string AuthorProfileImageUrl { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsApproved { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(e => e.AuthorProfileImageUrl, y => y.MapFrom(e => e.Creator.ProfileImageUrl));
        }
    }
}
