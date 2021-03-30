namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {

        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Content { get; set; }

        public string Title { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
