namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string AuthorName { get; set; }

        public int Id { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public IEnumerable<CommentViewModel> Replies { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                 .ForMember(x => x.AuthorName, y => y.MapFrom(c => c.Author.UserName));
        }
    }
}
