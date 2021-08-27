using AutoMapper;
using Ganss.XSS;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Data.Models.Enumerations;
using OnlineCourseManagementSystem.Services.Mapping;
using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
using OnlineCourseManagementSystem.Web.ViewModels.Skills;
using OnlineCourseManagementSystem.Web.ViewModels.Tags;
using OnlineCourseManagementSystem.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    public class CourseDetailsViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TrailerRemoteUrl { get; set; }

        public string FileRemoteUrl { get; set; }

        public string Description { get; set; }

        public double AverageRaiting => this.Reviews.Average(r => r.Rating);

        public int RecommendedDuration { get; set; }

        public DateTime StartDate { get; set; }

        public int UsersCount { get; set; }

        public int LecturesCount { get; set; }

        public CourseLevel Level { get; set; }

        public IEnumerable<UserCourse> Users { get; set; }

        [IgnoreMap]
        public IEnumerable<AllTagsByCourseIdViewModel> Tags { get; set; }

        [IgnoreMap]
        public IEnumerable<AllSkillsByCourseIdViewModel> Skills { get; set; }

        [IgnoreMap]
        public IEnumerable<AllReviewsByCourseIdViewModel> Reviews { get; set; }

        [IgnoreMap]
        public IEnumerable<AllLecturersByCourseIdViewModel> Lecturers { get; set; }

        [IgnoreMap]
        public IEnumerable<AllRecommendedCoursesByIdViewModel> RecommendedCourses { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
