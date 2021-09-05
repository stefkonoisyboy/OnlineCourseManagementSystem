namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class ContactMessage : BaseDeletableModel<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Content { get; set; }

        public bool? IsSeen { get; set; }

        public string SeenByUserId { get; set; }

        public virtual ApplicationUser SeenByUser { get; set; }
    }
}
