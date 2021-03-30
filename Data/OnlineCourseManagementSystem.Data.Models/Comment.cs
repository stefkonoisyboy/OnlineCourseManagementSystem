namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public Comment()
        {
            this.Likes = new HashSet<Like>();
            this.Dislikes = new HashSet<Dislike>();
        }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int? LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public int? PostId { get; set; }

        public virtual Post Post { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Dislike> Dislikes { get; set; }
    }
}
