namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.MessageQAs;

    public interface IMessageQAsService
    {
        /// <summary>
        /// Create message.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateMessageQAInputModel input);

        /// <summary>
        /// Create reply.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateReplyAsync(CreateReplyInputModel input);

        /// <summary>
        /// Create like.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task CreateLikeAsync(string creatorId, int messageId);

        /// <summary>
        /// Update message.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EditMessageQAInputModel input);

        /// <summary>
        /// Withdraw message.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task WithdrawAsync(int messageId);

        /// <summary>
        /// Delete message.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task DeleteAsync(int messageId);

        /// <summary>
        /// Delete like.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task DeleteLikeAsync(string creatorId, int messageId);

        /// <summary>
        /// Mark as starred.
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task MarkAsStarredAsync(int messageId, bool status);

        /// <summary>
        /// Mark as highlighted.
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task MarkAsHighlightedAsync(int messageId, bool status);

        /// <summary>
        /// Mark as answered.
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task MarkAsAnsweredAsync(int messageId, bool status);

        /// <summary>
        /// Archive message.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task ArchiveAsync(int messageId);

        /// <summary>
        /// Restore message.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task RestoreAsync(int messageId);

        /// <summary>
        /// Archive all.
        /// </summary>
        /// <returns></returns>
        Task ArchiveAllAsync();

        /// <summary>
        /// Restore all.
        /// </summary>
        /// <returns></returns>
        Task RestoreAllAsync();

        /// <summary>
        /// Check if user liked message.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        bool HasUserLikedMessage(string creatorId, int messageId);

        /// <summary>
        /// Get starred status.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        bool GetStarredStatus(int messageId);

        /// <summary>
        /// Check highlighted status.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        bool GetHighlightedStatus(int messageId);

        /// <summary>
        /// Check answered status.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        bool GetAnsweredStatus(int messageId);

        /// <summary>
        /// Get count of replies for given message.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        int GetCountOfRepliesForGivenMessage(int messageId);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageId"></param>
        /// <returns></returns>
        T GetById<T>(int messageId);

        /// <summary>
        /// Get all messages by channel id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channelId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllMessagesByChannelId<T>(int channelId);

        /// <summary>
        /// Get all archived messages by channel id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channelId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllArchivedMessagesByChannelId<T>(int channelId);

        /// <summary>
        /// Get all recent messages by channeld id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channelId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllRecentMessagesByChannelId<T>(int channelId);

        /// <summary>
        /// Get all replies by parent id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllRepliesByParentId<T>(int parentId);

        /// <summary>
        /// Get all replies.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllReplies<T>();
    }
}
