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
    }
}
