namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class MessageEmoji : BaseDeletableModel<int>
    {
        public int MessageId { get; set; }

        public virtual Message Message { get; set; }

        public int EmojiId { get; set; }

        public virtual Emoji Emoji { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }
    }
}
