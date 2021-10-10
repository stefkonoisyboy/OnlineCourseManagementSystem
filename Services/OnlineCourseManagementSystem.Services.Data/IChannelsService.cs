using OnlineCourseManagementSystem.Web.ViewModels.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IChannelsService
    {
        Task CreateAsync(CreateChannelInputModel input);

        Task DeleteAsync(int id);

        Task JoinChannelAsync(JoinChannelInputModel input);

        bool IsUserInChannel(string userId, int channelId);

        T GetById<T>(int id);

        IEnumerable<T> GetAllByParticipantId<T>(string participantId);

        IEnumerable<T> GetAllByCreatorId<T>(string creatorId);
    }
}
