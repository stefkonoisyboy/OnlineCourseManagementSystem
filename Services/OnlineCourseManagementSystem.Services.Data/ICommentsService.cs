namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task CreateAsync(CreateCommentInputModel inputModel);

        IEnumerable<T> GetAllByPostId<T>(int postId);

        Task<int?> DeleteAsync(int commentId);

        T GetById<T>(int commentId);

        Task UpdateAsync(EditCommentInputModel inputModel);

        Task ReplyToComment(ReplyToCommentInputModel inputModel);

        int? GetPostId(int commentId);

        IEnumerable<T> GetAllReplies<T>(int commentId);

        T GetLastActiveCommentByPostId<T>(int postId);

        Task Like(int commentId, string userId);

        Task Dislike(int commentId, string userId);
    }
}
