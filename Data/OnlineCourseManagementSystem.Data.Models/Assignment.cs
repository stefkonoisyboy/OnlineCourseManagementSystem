namespace OnlineCourseManagementSystem.Data.Models
{
    using OnlineCourseManagementSystem.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Assignment : BaseDeletableModel<int>
    {
        public Assignment()
        {
            this.Files = new HashSet<File>();
        }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public string Instructions { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? Points { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
