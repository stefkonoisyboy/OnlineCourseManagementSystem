namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Exam : BaseDeletableModel<int>
    {
        public Exam()
        {
            this.Users = new HashSet<UserExam>();
            this.Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PassMarks { get; set; }

        public string LecturerId { get; set; }

        public virtual ApplicationUser Lecturer { get; set; }

        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int? LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate => this.StartDate.AddMinutes(this.Duration);

        public int Duration { get; set; }

        public bool IsActive => DateTime.UtcNow >= this.StartDate && DateTime.UtcNow <= this.EndDate;

        public bool? IsCertificated { get; set; }

        public virtual ICollection<UserExam> Users { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
