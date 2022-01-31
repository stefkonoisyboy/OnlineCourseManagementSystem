namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.Orders = new HashSet<Order>();
            this.Exams = new HashSet<Exam>();
            this.Lectures = new HashSet<Lecture>();
            this.Users = new HashSet<UserCourse>();
            this.Posts = new HashSet<Post>();
            this.Assignments = new HashSet<Assignment>();
            this.Tags = new HashSet<CourseTag>();
            this.Lecturers = new HashSet<CourseLecturer>();
            this.Absences = new HashSet<Absence>();
            this.Skills = new HashSet<Skill>();
            this.Reviews = new HashSet<Review>();
            this.Certificates = new HashSet<Certificate>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CreatorId { get; set; }

        public int MachineLearningId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int RecommendedDuration { get; set; }

        public string TrailerRemoteUrl { get; set; }

        public int? SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public CourseLevel Level { get; set; }

        public int? FileId { get; set; }

        public virtual File File { get; set; }

        public bool? IsApproved { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<UserCourse> Users { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<CourseTag> Tags { get; set; }

        public virtual ICollection<CourseLecturer> Lecturers { get; set; }

        public virtual ICollection<Absence> Absences { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}
