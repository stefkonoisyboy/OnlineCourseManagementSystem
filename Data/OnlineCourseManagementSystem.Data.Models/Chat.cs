namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Chat : BaseDeletableModel<int>
    {
        public Chat()
        {
            this.Users = new HashSet<ChatUser>();
            this.Messages = new HashSet<Message>();
        }

        public string Name { get; set; }

        public virtual ICollection<ChatUser> Users { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
