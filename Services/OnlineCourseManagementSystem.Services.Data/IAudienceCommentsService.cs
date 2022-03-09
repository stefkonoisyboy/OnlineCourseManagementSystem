namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.AudienceComments;

    public interface IAudienceCommentsService
    {
        /// <summary>
        /// Create comment.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateAudienceCommentInputModel input);

        /// <summary>
        /// Get all by channel code.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByChannelCode<T>(string code);
    }
}
