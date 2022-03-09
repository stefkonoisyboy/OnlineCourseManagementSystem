namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Completition : BaseDeletableModel<int>
    {
        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
