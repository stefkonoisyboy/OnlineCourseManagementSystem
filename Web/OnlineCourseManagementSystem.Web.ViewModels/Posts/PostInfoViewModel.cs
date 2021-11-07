namespace OnlineCourseManagementSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Comments;

    public class PostInfoViewModel : IMapFrom<Post>
    {
        public PostViewModel Post { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public CreateCommentInputModel InputModel { get; set; }

        public ReplyToCommentInputModel ReplyInputModel { get; set; }
    }
}
