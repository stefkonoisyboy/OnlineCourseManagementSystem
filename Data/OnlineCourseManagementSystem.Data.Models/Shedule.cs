namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Shedule : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public virtual Event Event { get; set; }

        public int EventId { get; set; }
    }
}
