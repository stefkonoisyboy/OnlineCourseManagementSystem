namespace OnlineCourseManagementSystem.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using OnlineCourseManagementSystem.Web.ViewModels.VideoConferences;

    public class VideoHub : Hub
    {
        public async Task RoomsUpdated(string room)
        {
            await this.Clients.All.SendAsync(HubEndpoints.RoomsUpdated, room);
        }

        public async Task AddParticipant(string name, string participantId)
        {
            await this.Clients.All.SendAsync("AddParticipant", name, participantId);
        }

        public async Task AllowDisplayVideo(string roomId, string participantId)
        {
            await this.Clients.All.SendAsync("AllowDisplayVideo", roomId, participantId);
        }

        public async Task AllowUnmuteMic(string roomId, string participantId)
        {
            await this.Clients.All.SendAsync("AllowUnmuteMic", roomId, participantId);
        }

        public async Task AllowShareScreen(string roomId,string participantId)
        {
            await this.Clients.All.SendAsync("AllowShareScreen", roomId, participantId);
        }
    }
}
