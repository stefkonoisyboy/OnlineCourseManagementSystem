namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Like : BaseDeletableModel<int>
    {
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int? PostId { get; set; }

        public virtual Post Post { get; set; }

        public int? CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public int? AudienceCommentId { get; set; }

        public virtual AudienceComment AudienceComment { get; set; }

        public int? MessageQAId { get; set; }

        public virtual MessageQA MessageQA { get; set; }
    }
}
