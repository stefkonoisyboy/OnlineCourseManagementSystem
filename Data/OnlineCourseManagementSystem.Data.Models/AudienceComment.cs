namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class AudienceComment : BaseDeletableModel<int>
    {
        public AudienceComment()
        {
            this.Likes = new HashSet<Like>();
        }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int? ParentId { get; set; }

        public virtual AudienceComment Parent { get; set; }

        public int ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}
