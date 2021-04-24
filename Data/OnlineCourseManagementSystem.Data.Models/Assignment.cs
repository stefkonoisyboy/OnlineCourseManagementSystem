namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Assignment : BaseDeletableModel<int>
    {
        public Assignment()
        {
            this.Files = new HashSet<File>();
            this.Users = new HashSet<UserAssignment>();
        }

        public string Title { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string Instructions { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? PossiblePoints { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<UserAssignment> Users { get; set; }
    }
}
