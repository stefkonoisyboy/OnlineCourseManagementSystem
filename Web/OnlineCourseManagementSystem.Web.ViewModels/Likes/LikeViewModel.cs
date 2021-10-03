namespace OnlineCourseManagementSystem.Web.ViewModels.Likes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class LikeViewModel : IMapFrom<Like>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Like, LikeViewModel>()
                .ForMember(x => x.UserId, y => y.MapFrom(l => l.Creator.Id));
        }
    }
}
