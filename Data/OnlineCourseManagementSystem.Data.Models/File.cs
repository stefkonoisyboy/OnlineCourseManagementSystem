namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class File : BaseDeletableModel<int>
    {
        public File()
        {
            this.FileCompletitions = new HashSet<FileCompletition>();
        }

        public string Extension { get; set; }

        public string RemoteUrl { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? AssignmentId { get; set; }

        public virtual Assignment Assignment { get; set; }

        public int? LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        public int? EventId { get; set; }

        public virtual Event Event { get; set; }

        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int? AlbumId { get; set; }

        public Album Album { get; set; }

        public FileType Type { get; set; }

        public bool IsCompleted { get; set; }

        public virtual ICollection<FileCompletition> FileCompletitions { get; set; }
    }
}
