namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class ChatUser : BaseDeletableModel<int>
    {
        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsPinned { get; set; }

        public bool IsMuted { get; set; }
    }
}
