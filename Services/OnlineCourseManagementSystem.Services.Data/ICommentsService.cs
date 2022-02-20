namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using OnlineCourseManagementSystem.Services.Data.MachineLearning;
    using OnlineCourseManagementSystem.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        /// <summary>
        /// This method creates comment.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task CreateAsync(CreateCommentInputModel inputModel);

        /// <summary>
        /// This method gets all comments for post.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="postId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByPostId<T>(int postId);

        /// <summary>
        /// This method deletes comment.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<int?> DeleteAsync(int commentId);

        /// <summary>
        ///  This method gets comment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commentId"></param>
        /// <returns></returns>
        T GetById<T>(int commentId);

        /// <summary>
        /// This method updates comment.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task UpdateAsync(EditCommentInputModel inputModel);

        /// <summary>
        /// This method replies on comment.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task ReplyToComment(ReplyToCommentInputModel inputModel);

        /// <summary>
        /// This method gets comment's postId.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        int? GetPostId(int commentId);

        /// <summary>
        /// This method gets all replies for comment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commentId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllReplies<T>(int commentId);

        /// <summary>
        /// This method gets lastActive comment for post.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="postId"></param>
        /// <returns></returns>
        T GetLastActiveCommentByPostId<T>(int postId);

        /// <summary>
        /// This method likes comment by user.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task Like(int commentId, string userId);

        /// <summary>
        /// This method dislikes comment by user.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task Dislike(int commentId, string userId);

        /// <summary>
        /// This method gets toxic comments(founded abusive or offensive based on ml.net prediction).
        /// </summary>
        /// <returns></returns>
        IEnumerable<PredictedCommentViewModel> GetAllCommentsClassified();

        /// <summary>
        /// This method deletes all toxic comments
        /// </summary>
        /// <param name="toxicComments"></param>
        Task DeleteAllToxicComments();
    }
}
