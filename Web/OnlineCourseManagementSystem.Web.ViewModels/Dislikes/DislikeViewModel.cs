namespace OnlineCourseManagementSystem.Web.ViewModels.Dislikes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class DislikeViewModel : IMapFrom<Dislike>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Dislike, DislikeViewModel>()
                .ForMember(x => x.UserId, y => y.MapFrom(d => d.Creator.Id));
        }
    }
}
