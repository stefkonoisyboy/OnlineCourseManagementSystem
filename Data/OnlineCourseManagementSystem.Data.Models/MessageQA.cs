namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class MessageQA : BaseDeletableModel<int>
    {
        public MessageQA()
        {
            this.Likes = new HashSet<Like>();
        }

        public string Content { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int? ParentId { get; set; }

        public virtual MessageQA Parent { get; set; }

        public int ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        public bool? IsHighlighted { get; set; }

        public bool? IsStarred { get; set; }

        public bool? IsAnswered { get; set; }

        public bool IsArchived { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}
