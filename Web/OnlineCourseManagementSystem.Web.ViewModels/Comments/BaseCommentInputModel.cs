namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BaseCommentInputModel
    {
        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int? PostId { get; set; }
    }
}
