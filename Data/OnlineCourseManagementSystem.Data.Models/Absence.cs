namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class Absence : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        public AbsenceType Type { get; set; }

        public string Reason { get; set; }
    }
}
