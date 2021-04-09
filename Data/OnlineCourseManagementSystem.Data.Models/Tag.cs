namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Courses = new HashSet<CourseTag>();
        }

        public string Name { get; set; }

        public virtual ICollection<CourseTag> Courses { get; set; }
    }
}
