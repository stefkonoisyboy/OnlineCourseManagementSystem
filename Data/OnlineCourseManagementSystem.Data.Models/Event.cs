namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.Files = new HashSet<File>();
            this.Shedules = new HashSet<Shedule>();
        }

        public string Theme { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public bool? IsApproved { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Shedule> Shedules { get; set; }
    }
}
