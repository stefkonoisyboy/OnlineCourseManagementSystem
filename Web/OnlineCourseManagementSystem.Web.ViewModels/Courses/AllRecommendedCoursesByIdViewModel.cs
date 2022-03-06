namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllRecommendedCoursesByIdViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string FileRemoteUrl { get; set; }

        public string Name { get; set; }

        public double AverageRaiting { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, AllRecommendedCoursesByIdViewModel>()
                .ForMember(all => all.AverageRaiting, opt => opt.MapFrom(c => c.Reviews.Count() > 0 ? c.Reviews.Average(r => r.Rating) : 0));
        }
    }
}
