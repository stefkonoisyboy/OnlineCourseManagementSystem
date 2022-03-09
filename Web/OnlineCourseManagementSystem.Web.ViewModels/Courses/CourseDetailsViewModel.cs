namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Ganss.XSS;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Paging;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
    using OnlineCourseManagementSystem.Web.ViewModels.Skills;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class CourseDetailsViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public double Rating => this.Reviews.Count() == 0 ? 0 : this.Reviews.Average(r => r.Rating);

        [IgnoreMap]
        public int CompletedLecturesCount { get; set; }

        public int LecturesCount { get; set; }

        public IEnumerable<UserCourse> Users { get; set; }

        [IgnoreMap]
        public IEnumerable<AllSkillsByCourseIdViewModel> Skills { get; set; }

        [IgnoreMap]
        public IEnumerable<AllReviewsByCourseIdViewModel> Reviews { get; set; }

        [IgnoreMap]
        public IEnumerable<AllLecturersByCourseIdViewModel> Lecturers { get; set; }

        [IgnoreMap]
        public AllLecturesByIdListViewModel ListOfLectures { get; set; }

        [IgnoreMap]
        public CurrentUserViewModel CurrentUser { get; set; }
    }
}
