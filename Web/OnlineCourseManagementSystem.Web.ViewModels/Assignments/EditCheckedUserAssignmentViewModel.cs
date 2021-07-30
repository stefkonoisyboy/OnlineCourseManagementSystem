﻿namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class EditCheckedUserAssignmentViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {
        public int AssignmentId { get; set; }

        public EditCheckedAssignmentInputModel InputModel { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public AssignmentInfoViewModel AssignmentInfo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, EditCheckedUserAssignmentViewModel>()
                 .ForMember(x => x.Username, y => y.MapFrom(ua => $"{ua.User.FirstName} {ua.User.LastName}"));
        }
    }
}
