namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ImageViewModel : IMapFrom<File>, IHaveCustomMappings
    {
        public int ImageId { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<File, ImageViewModel>()
                .ForMember(f => f.ImageUrl, y => y
                .MapFrom(x => x.RemoteUrl))
                .ForMember(f => f.ImageId, y =>
                  y.MapFrom(x => x.Id));
        }
    }
}
