namespace OnlineCourseManagementSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class BasePostInputModel
    {
        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        [Required]
        [MinLength(10)]
        public string Title { get; set; }

        [Required]
        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> CourseItems { get; set; }

        public string AuthorId { get; set; }
    }
}
