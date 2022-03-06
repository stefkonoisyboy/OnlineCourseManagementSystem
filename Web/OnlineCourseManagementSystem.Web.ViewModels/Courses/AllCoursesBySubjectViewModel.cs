namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class AllCoursesBySubjectViewModel : BasePagingViewModel, IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RecommendedDuration { get; set; }

        public string FileRemoteUrl { get; set; }

        public DateTime StartDate { get; set; }

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
