namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Ganss.XSS;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Skills;

    public class CourseByIdViewModel : IMapFrom<Course>
    {
        public CourseByIdViewModel()
        {
            this.Skills = new List<AllSkillsByCourseIdViewModel>().ToList();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FileRemoteUrl { get; set; }

        [IgnoreMap]
        public IEnumerable<AllLecturersByIdViewModel> Lecturers { get; set; }

        [IgnoreMap]
        public IEnumerable<AllLecturesByIdViewModel> Lectures { get; set; }

        [IgnoreMap]
        public IEnumerable<AllSkillsByCourseIdViewModel> Skills { get; set; }
    }
}
