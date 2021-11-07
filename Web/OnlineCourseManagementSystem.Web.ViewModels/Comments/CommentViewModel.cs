namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Dislikes;
    using OnlineCourseManagementSystem.Web.ViewModels.Likes;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public int Id { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }

        public IEnumerable<LikeViewModel> Likes { get; set; }

        public IEnumerable<DislikeViewModel> Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public IEnumerable<CommentViewModel> Replies { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                 .ForMember(x => x.AuthorName, y => y.MapFrom(c => $"{c.Author.FirstName} {c.Author.LastName}"));
        }
    }
}
