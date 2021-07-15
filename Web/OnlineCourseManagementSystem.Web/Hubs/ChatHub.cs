namespace OnlineCourseManagementSystem.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string name, string message, int room)
        {
            await this.Clients.All.SendAsync("SendMessage", name, message, room);
        }
    }
}
