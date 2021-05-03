namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditCommentInputModel : BaseCommentInputModel, IMapFrom<Comment>, IHaveCustomMappings
    {
        public int CommentId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, EditCommentInputModel>()
                .ForMember(x => x.CommentId, y => y.MapFrom(c => c.Id));
        }
    }
}
