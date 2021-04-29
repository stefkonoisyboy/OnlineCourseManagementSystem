namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Student : BaseDeletableModel<string>
    {
        public Student()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Absences = new HashSet<Absence>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ParentId { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual ICollection<Absence> Absences { get; set; }
    }
}
