namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditAssignmentInputModel : BaseAssignmentInputModel, IMapFrom<Assignment>, IHaveCustomMappings
    {
        public int AssignmentId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Assignment, EditAssignmentInputModel>()
                .ForMember(x => x.AssignmentId, y => y.MapFrom(a => a.Id));
        }
    }
}
