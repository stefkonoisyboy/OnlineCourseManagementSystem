namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Channels;

    public interface IChannelsService
    {
        /// <summary>
        /// Create channel.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateChannelInputModel input);

        /// <summary>
        /// Delete channel.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Join channel.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task JoinChannelAsync(JoinChannelInputModel input);

        /// <summary>
        /// Checks if user is in channel.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        bool IsUserInChannel(string userId, int channelId);

        /// <summary>
        /// Get channel name by id.
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        string GetChannelNameById(int channelId);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Get all by participant.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="participantId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByParticipantId<T>(string participantId);

        /// <summary>
        /// Get all by creator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCreatorId<T>(string creatorId);
    }
}
