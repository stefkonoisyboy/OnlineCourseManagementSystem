namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Lecture : BaseDeletableModel<int>
    {
        public Lecture()
        {
            this.Files = new HashSet<File>();
            this.Comments = new HashSet<Comment>();
            this.Assignments = new HashSet<Assignment>();
            this.Absences = new HashSet<Absence>();
            this.Exams = new HashSet<Exam>();
            this.Completitions = new HashSet<Completition>();
        }

        public string Description { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public bool IsCompleted { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<Absence> Absences { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public virtual ICollection<Completition> Completitions { get; set; }
    }
}
