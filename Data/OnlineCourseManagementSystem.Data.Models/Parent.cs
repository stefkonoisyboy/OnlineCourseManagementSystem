namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Parent : BaseDeletableModel<string>
    {
        public Parent()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Students = new HashSet<Student>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
