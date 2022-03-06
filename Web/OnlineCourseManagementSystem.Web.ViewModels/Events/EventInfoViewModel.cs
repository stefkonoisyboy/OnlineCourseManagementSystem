namespace OnlineCourseManagementSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using OnlineCourseManagementSystem.Web.ViewModels.Shedules;

    public class EventInfoViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public string Theme { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Address { get; set; }

        public IEnumerable<FileViewModel> Files { get; set; }

        public IEnumerable<SheduleViewModel> Shedules { get; set; }

        public string CreatorFirstName { get; set; }

        public string CreatorLastName { get; set; }

        public string CreatorDescription { get; set; }

        public string CreatorProfileImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventInfoViewModel>()
                .ForMember(x => x.CreatorDescription, y => y.MapFrom(e => e.Creator.Background));
        }
    }
}
