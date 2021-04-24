namespace OnlineCourseManagementSystem.Web.ViewModels.Albums
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AlbumViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public int AlbumId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Album, AlbumViewModel>()
                .ForMember(
                    x => x.AlbumId,
                    y => y.MapFrom(x => x.Id));
        }
    }
}
