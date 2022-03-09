namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllFilesByLectureIdViewModel : IMapFrom<File>
    {
        public int Id { get; set; }

        public string Extension { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string LectureTitle { get; set; }

        public int LectureId { get; set; }

        public int LectureCourseId { get; set; }

        public bool IsCompleted { get; set; }

        public ICollection<FileCompletition> FileCompletitions { get; set; }
    }
}
