namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class AssignmentPageViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {
        public int AssignmentId { get; set; }

        public string Instructions { get; set; }

        public int Points { get; set; }

        public int PossiblePoints { get; set; }

        public string Feedback { get; set; }

        [IgnoreMap]
        public IEnumerable<FileAssignmentViewModel> ResourceFiles { get; set; }

        [IgnoreMap]
        public IEnumerable<FileAssignmentViewModel> WorkFiles { get; set; }

        public string CourseName { get; set; }

        public FilesToAssignmentInputModel InputModel { get; set; }

        public DateTime? TurnedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, AssignmentPageViewModel>()
                .ForMember(x => x.CourseName, y => y.MapFrom(ua => ua.Assignment.Course.Name))
                .ForMember(x => x.AssignmentId, y => y.MapFrom(ua => ua.AssignmentId))
                .ForMember(x => x.TurnedOn, y => y.MapFrom(ua => ua.TurnedOn))
                .ForMember(x => x.Points, y => y.MapFrom(ua => ua.Points))
                .ForMember(x => x.PossiblePoints, y => y.MapFrom(ua => ua.Assignment.PossiblePoints));
        }
    }
}
