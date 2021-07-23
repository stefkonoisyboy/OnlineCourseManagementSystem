namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public Message()
        {
            this.Emojis = new HashSet<MessageEmoji>();
        }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public string Content { get; set; }

        public int? ParentId { get; set; }

        public virtual Message Parent { get; set; }

        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; }

        public bool IsSeen { get; set; }

        public virtual ICollection<MessageEmoji> Emojis { get; set; }
    }
}
