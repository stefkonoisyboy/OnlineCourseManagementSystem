namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllActiveCoursesViewModel : AllUpcomingCoursesViewModel, IHaveCustomMappings
    {
        public string Description { get; set; }

        public decimal Price { get; set; }

        [IgnoreMap]
        public double Rating { get; set; }

        public string SubjectName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, AllActiveCoursesViewModel>()
                .ForMember(top => top.Rating, opt => opt.MapFrom(c => c.Reviews.Count() == 0 ? 0 : c.Reviews.Average(r => r.Rating)));
        }
    }
}
