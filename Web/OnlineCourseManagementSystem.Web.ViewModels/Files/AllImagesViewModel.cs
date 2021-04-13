using AutoMapper;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    public class AllImagesViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public string AlbumName { get; set; }

        public IEnumerable<ImageViewModel> Images { get; set; }

        public int AlbumId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Album, AllImagesViewModel>()
                 .ForMember(x => x.AlbumName, y => y.MapFrom(x => x.Name))
                 .ForMember(x => x.AlbumId, y => y.MapFrom(x => x.Id));
        }
    }
}
