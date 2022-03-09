namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;

    public class VideoByIdViewModel : IMapFrom<File>
    {
        public int Id { get; set; }

        public string RemoteUrl { get; set; }

        public string LectureTitle { get; set; }

        public string LectureCourseName { get; set; }

        public int LectureCourseId { get; set; }

        public DateTime LectureCourseStartDate { get; set; }

        [IgnoreMap]
        public IEnumerable<AllLecturesByIdViewModel> Lectures { get; set; }
    }
}
