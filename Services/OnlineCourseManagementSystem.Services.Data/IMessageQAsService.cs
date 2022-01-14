using OnlineCourseManagementSystem.Web.ViewModels.MessageQAs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IMessageQAsService
    {
        Task CreateAsync(CreateMessageQAInputModel input);

        Task CreateReplyAsync(CreateReplyInputModel input);

        Task CreateLikeAsync(string creatorId, int messageId);

        Task UpdateAsync(EditMessageQAInputModel input);

        Task WithdrawAsync(int messageId);

        Task DeleteAsync(int messageId);

        Task DeleteLikeAsync(string creatorId, int messageId);

        Task MarkAsStarredAsync(int messageId, bool status);

        Task MarkAsHighlightedAsync(int messageId, bool status);

        Task MarkAsAnsweredAsync(int messageId, bool status);

        Task ArchiveAsync(int messageId);

        Task RestoreAsync(int messageId);

        Task ArchiveAllAsync();

        Task RestoreAllAsync();

        bool HasUserLikedMessage(string creatorId, int messageId);

        bool GetStarredStatus(int messageId);

        bool GetHighlightedStatus(int messageId);

        bool GetAnsweredStatus(int messageId);

        int GetCountOfRepliesForGivenMessage(int messageId);

        T GetById<T>(int messageId);

        IEnumerable<T> GetAllMessagesByChannelId<T>(int channelId);

        IEnumerable<T> GetAllArchivedMessagesByChannelId<T>(int channelId);

        IEnumerable<T> GetAllRecentMessagesByChannelId<T>(int channelId);

        IEnumerable<T> GetAllRepliesByParentId<T>(int parentId);

        IEnumerable<T> GetAllReplies<T>();
    }
}
