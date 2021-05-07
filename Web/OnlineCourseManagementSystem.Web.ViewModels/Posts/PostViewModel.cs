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

        public string AuthorName { get; set; }

        public IEnumerable<LikeViewModel> Likes { get; set; }

        public IEnumerable<DislikeViewModel> Dislikes { get; set; }

        public int CourseId { get; set; }

        public LastActiveViewModel LastActive { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.AuthorName, y => y.MapFrom(p => p.Author.UserName))
                .ForMember(x => x.Id, y => y.MapFrom(p => p.Id));
        }
    }
}
