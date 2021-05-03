using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    public class BaseCommentInputModel
    {
        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int? PostId { get; set; }
    }
}
