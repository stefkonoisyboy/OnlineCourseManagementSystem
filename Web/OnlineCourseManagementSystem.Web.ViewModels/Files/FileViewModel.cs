namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class FileViewModel : IMapFrom<File>, IHaveCustomMappings
    {
        public string RemoteUrl { get; set; }

        public string Extension { get; set; }

        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<File, FileAssignmentViewModel>()
                .ForMember(x => x.FileId, y => y.MapFrom(f => f.Id));
        }
    }
}
