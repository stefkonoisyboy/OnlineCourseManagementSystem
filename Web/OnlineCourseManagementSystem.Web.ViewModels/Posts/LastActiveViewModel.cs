namespace OnlineCourseManagementSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class LastActiveViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public DateTime LastActive { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, LastActiveViewModel>()
                .ForMember(x => x.LastActive, y => y.MapFrom(c => c.ModifiedOn ?? c.CreatedOn))
                .ForMember(x => x.Name, y => y.MapFrom(c => $"{c.Author.FirstName} {c.Author.LastName}"));
        }
    }
}
