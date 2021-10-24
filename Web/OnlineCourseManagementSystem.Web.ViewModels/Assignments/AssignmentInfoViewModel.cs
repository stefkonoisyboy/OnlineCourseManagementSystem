namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class AssignmentInfoViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Instructions { get; set; }

        public int? PossiblePoints { get; set; }

        public DateTime? TurnedOn { get; set; }

        public bool Seen { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<FileAssignmentViewModel> WorkFiles { get; set; }

        public IEnumerable<FileAssignmentViewModel> ResourceFiles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, AssignmentInfoViewModel>()
                .ForMember(a => a.Title, x => x.MapFrom(ua => ua.Assignment.Title))
                .ForMember(a => a.Instructions, x => x.MapFrom(ua => ua.Assignment.Instructions))
                .ForMember(a => a.PossiblePoints, x => x.MapFrom(ua => ua.Assignment.PossiblePoints))
                .ForMember(a => a.StartDate, x => x.MapFrom(ua => ua.Assignment.StartDate))
                .ForMember(a => a.EndDate, x => x.MapFrom(ua => ua.Assignment.EndDate));
        }
    }
}
