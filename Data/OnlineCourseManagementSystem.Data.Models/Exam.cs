namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Exam : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string LecturerId { get; set; }

        public virtual ApplicationUser Lecturer { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
