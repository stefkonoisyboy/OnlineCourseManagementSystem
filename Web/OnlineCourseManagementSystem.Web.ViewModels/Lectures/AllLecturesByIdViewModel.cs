namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class AllLecturesByIdViewModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string VideoRoomId { get; set; }

        public IEnumerable<AllFilesByLectureIdViewModel> Files { get; set; }

        public IEnumerable<AllExamsByLectureIdViewModel> Exams { get; set; }
    }
}
