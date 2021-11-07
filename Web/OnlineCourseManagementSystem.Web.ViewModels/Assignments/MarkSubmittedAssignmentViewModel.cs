namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class MarkSubmittedAssignmentViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {
        public int AssignmentId { get; set; }

        public int CourseId { get; set; }

        public MarkSubmittedAssignmentInputModel InputModel { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public UserViewModel User { get; set; }

        public AssignmentInfoViewModel AssignmentInfo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, MarkSubmittedAssignmentViewModel>()
                 .ForMember(x => x.Username, y => y.MapFrom(ua => $"{ua.User.FirstName} {ua.User.LastName}"))
                 .ForMember(x => x.CourseId, y => y.MapFrom(ua => ua.Assignment.CourseId));
        }
    }
}
