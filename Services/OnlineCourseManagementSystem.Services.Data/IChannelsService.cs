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

        IEnumerable<T> GetAllByParticipantId<T>(string participantId);

        IEnumerable<T> GetAllByCreatorId<T>(string creatorId);
    }
}
