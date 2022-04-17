namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.VideoConferences;

    public interface IRoomsService
    {
        Task<string> CreateRoomAsync(CreateRoomInputModel inputModel);

        T GetRoomById<T>(string roomId);

        Task<string> AddParticipantsToRoom(string roomId, string userIdentityName, bool isCreator = false);

        T GetRoomParticipant<T>(string participantId);

        Task GiveRightToDisplayCamera(string participantId);

        Task GiveRightToUnMuteMic(string participantId);

        Task GiveRightToShareScreen(string participantId);

        Task DontGiveRightToDisplayCamera(string participantId);

        Task DontGiveRightToUnMuteMic(string participantId);

        Task DontGiveRightToShareScreen(string participantId);
    }
}
