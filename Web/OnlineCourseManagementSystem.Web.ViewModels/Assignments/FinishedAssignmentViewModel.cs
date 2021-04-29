namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class FinishedAssignmentViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {
        public int AssignmentId { get; set; }

        public string Title { get; set; }

        public string CourseName { get; set; }

        public int PossiblePoints { get; set; }

        public int ReceivedPoints { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, FinishedAssignmentViewModel>()
                .ForMember(a => a.CourseName, y => y.MapFrom(a => a.Assignment.Course.Name))
                .ForMember(a => a.PossiblePoints, y => y.MapFrom(a => a.Assignment.PossiblePoints))
                .ForMember(a => a.AssignmentId, y => y.MapFrom(a => a.AssignmentId))
                .ForMember(a => a.ReceivedPoints, y => y.MapFrom(a => a.Points))
                .ForMember(a => a.Title, y => y.MapFrom(a => a.Assignment.Title));
        }
    }
}
