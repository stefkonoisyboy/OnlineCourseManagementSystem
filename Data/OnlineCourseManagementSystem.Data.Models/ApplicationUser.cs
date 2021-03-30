// ReSharper disable VirtualMemberCallInConstructor
namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using OnlineCourseManagementSystem.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Events = new HashSet<Event>();
            this.Orders = new HashSet<Order>();
            this.LecturerExams = new HashSet<Exam>();
            this.Exams = new HashSet<UserExam>();
            this.Files = new HashSet<File>();
            this.Assignments = new HashSet<UserAssignment>();
            this.Courses = new HashSet<UserCourse>();
            this.Comments = new HashSet<Comment>();
            this.Answers = new HashSet<Answer>();
            this.Likes = new HashSet<Like>();
            this.Dislikes = new HashSet<Dislike>();
            this.Posts = new HashSet<Post>();

            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Background { get; set; }

        public DateTime BirthDate { get; set; }

        public string TownId { get; set; }

        public virtual Town Town { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Exam> LecturerExams { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<UserAssignment> Assignments { get; set; }

        public virtual ICollection<UserCourse> Courses { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserExam> Exams { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Dislike> Dislikes { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
