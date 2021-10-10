namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllCoursesByUserViewModel : IMapFrom<UserCourse>, IHaveCustomMappings
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public decimal CoursePrice { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }

        public string CourseFileRemoteUrl { get; set; }

        public string CourseSubjectName { get; set; }

        public int CourseLecturesCount { get; set; }

        [IgnoreMap]
        public double CompletedLecturesCount { get; set; }

        public double Progress => (double)((double)(this.CompletedLecturesCount / this.CourseLecturesCount) * 100);

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserCourse, AllCoursesByUserViewModel>()
                .ForMember(all => all.CompletedLecturesCount, opt => opt.MapFrom(c => (double)c.Course.Lectures.Count(l => l.IsCompleted)));
        }
    }
}
