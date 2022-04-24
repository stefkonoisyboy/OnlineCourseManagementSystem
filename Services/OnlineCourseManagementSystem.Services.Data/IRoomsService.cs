namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.VideoConferences;

    public interface IRoomsService
    {
        /// <summary>
        /// Creates Room.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task<string> CreateRoomAsync(CreateRoomInputModel inputModel);

        /// <summary>
        /// Gets room by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="roomId"></param>
        /// <returns></returns>
        T GetRoomById<T>(string roomId);

        /// <summary>
        /// Adds Participant to room.
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="userIdentityName"></param>
        /// <param name="isCreator"></param>
        /// <returns></returns>
        Task<string> AddParticipantsToRoom(string roomId, string userIdentityName, bool isCreator = false);

        /// <summary>
        /// Gets participants.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="participantId"></param>
        /// <returns></returns>
        T GetRoomParticipant<T>(string participantId);

        /// <summary>
        /// Gives right to participant to display camera.
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns></returns>
        Task GiveRightToDisplayCamera(string participantId);

        /// <summary>
        /// Gives right to participant to unmute mic.
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns></returns>
        Task GiveRightToUnMuteMic(string participantId);

        /// <summary>
        /// Gives right to participant to share screen.
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns></returns>
        Task GiveRightToShareScreen(string participantId);

        /// <summary>
        /// Unallowes right to participant to display camera.
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns></returns>
        Task DontGiveRightToDisplayCamera(string participantId);

        /// <summary>
        /// Unallowes right to participant to unmute mic.
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns></returns>
        Task DontGiveRightToUnMuteMic(string participantId);

        /// <summary>
        /// Unallowes right to participant to share screen.
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns></returns>
        Task DontGiveRightToShareScreen(string participantId);

        Task UpdateAsync(UpdateRoomInputModel inputModel);
    }
}
