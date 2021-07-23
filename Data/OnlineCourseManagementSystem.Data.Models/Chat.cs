namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class Chat : BaseDeletableModel<int>
    {
        public Chat()
        {
            this.Users = new HashSet<ChatUser>();
            this.Messages = new HashSet<Message>();
        }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public string Name { get; set; }

        public ChatType ChatType { get; set; }

        public string IconRemoteUrl { get; set; }

        public virtual ICollection<ChatUser> Users { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
