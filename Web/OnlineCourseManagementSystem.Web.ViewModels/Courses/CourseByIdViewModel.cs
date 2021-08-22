namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Ganss.XSS;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Skills;

    public class CourseByIdViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FileRemoteUrl { get; set; }

        public IEnumerable<AllLecturersByIdViewModel> Lecturers { get; set; }

        public IEnumerable<AllLecturesByIdViewModel> Lectures { get; set; }

        public IEnumerable<AllSkillsByCourseIdViewModel> Skills { get; set; }
    }
}
