namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class CheckedUserAssignmentsViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {
        public int AssignmentId { get; set; }

        public string UserId { get; set; }

        public string StudentName { get; set; }

        public int Points { get; set; }

        public DateTime? TurnedOn { get; set; }

        public DateTime EndDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, CheckedUserAssignmentsViewModel>()
                .ForMember(x => x.StudentName, y => y.MapFrom(ua => $"{ua.User.FirstName} {ua.User.LastName}"));
        }
    }
}
