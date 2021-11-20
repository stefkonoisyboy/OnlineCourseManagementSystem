namespace OnlineCourseManagementSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Comments;
    using OnlineCourseManagementSystem.Web.ViewModels.Dislikes;
    using OnlineCourseManagementSystem.Web.ViewModels.Likes;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public IEnumerable<LikeViewModel> Likes { get; set; }

        public IEnumerable<DislikeViewModel> Dislikes { get; set; }

        public int CourseId { get; set; }

        [IgnoreMap]
        public LastActiveViewModel LastActive { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.AuthorName, y => y.MapFrom(p => $"{p.Author.FirstName} {p.Author.LastName}"))
                .ForMember(x => x.Id, y => y.MapFrom(p => p.Id))
                .ForMember(x => x.CommentsCount, y => y.MapFrom(p => p.Comments.Count));
        }
    }
}
