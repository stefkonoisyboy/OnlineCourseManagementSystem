namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

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
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public int FileId { get; set; }

        public virtual File File { get; set; }

        public bool? IsApproved { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<UserCourse> Users { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<CourseTag> Tags { get; set; }
    }
}
