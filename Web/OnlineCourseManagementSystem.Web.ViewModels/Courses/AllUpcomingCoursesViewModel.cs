using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    public class AllUpcomingCoursesViewModel : IMapFrom<Course>
    {
        public string Name { get; set; }

        public int RecommendedDuration { get; set; }

        public string FileRemoteUrl { get; set; }

        public DateTime StartDate { get; set; }
    }
}
