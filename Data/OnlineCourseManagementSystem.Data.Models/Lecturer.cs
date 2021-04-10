namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Lecturer : BaseDeletableModel<string>
    {
        public Lecturer()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Courses = new HashSet<CourseLecturer>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CourseLecturer> Courses { get; set; }
    }
}
