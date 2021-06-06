namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class MarkSubmittedAssignmentViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {
        public int AssignmentId { get; set; }

        public IEnumerable<FileAssignmentViewModel> WorkFiles { get; set; }

        public IEnumerable<FileAssignmentViewModel> ResourceFiles { get; set; }

        public MarkSubmittedAssignmentInputModel InputModel { get; set; }

        public string UserId { get; set; }

        public DateTime? TurnedOn { get; set; }

        public int? PossiblePoints { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, MarkSubmittedAssignmentViewModel>()
                 .ForMember(x => x.TurnedOn, y => y.MapFrom(ua => ua.TurnedOn))
                 .ForMember(x => x.PossiblePoints, y => y.MapFrom(ua => ua.Assignment.PossiblePoints));
        }
    }
}
