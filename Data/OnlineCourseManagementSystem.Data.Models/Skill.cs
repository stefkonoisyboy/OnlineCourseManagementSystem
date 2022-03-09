namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Skill : BaseDeletableModel<int>
    {
        public string Text { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
