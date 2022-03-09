namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class AssignmentUserInfoViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {
        public string StudentName { get; set; }

        public int AssignmentId { get; set; }

        public string UserId { get; set; }

        public bool Seen { get; set; }

        public DateTime TurnedOn { get; set; }

        public DateTime EndDate { get; set; }

        public bool Turned { get; set; }

        public bool IsChecked { get; set; }

        // public MarkSubmittedAssignmentInputModel InputModel { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, AssignmentUserInfoViewModel>()
                .ForMember(x => x.StudentName, y => y.MapFrom(ua => $"{ua.User.FirstName} {ua.User.LastName}"))
                .ForMember(x => x.Turned, y => y.MapFrom(ua => ua.TurnedOn != null))
                .ForMember(x => x.EndDate, y => y.MapFrom(ua => ua.Assignment.EndDate));
        }
    }
}
