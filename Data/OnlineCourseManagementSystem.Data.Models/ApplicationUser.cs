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
            this.Exams = new HashSet<Exam>();
            this.Files = new HashSet<File>();
            this.Assignments = new HashSet<UserAssignment>();
            this.Courses = new HashSet<UserCourse>();

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

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<UserAssignment> Assignments { get; set; }

        public virtual ICollection<UserCourse> Courses { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
