namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AssignmentPageViewModel : IMapFrom<Assignment>, IHaveCustomMappings
    {
        public string Instructions { get; set; }

        public int PossiblePoints { get; set; }

        public IEnumerable<File> Files { get; set; }

        public string CourseName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Assignment, AssignmentPageViewModel>()
                .ForMember(x => x.CourseName, y => y.MapFrom(c => c.Course.Name));
        }
    }
}
