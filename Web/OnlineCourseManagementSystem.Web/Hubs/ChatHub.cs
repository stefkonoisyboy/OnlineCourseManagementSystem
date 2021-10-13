namespace OnlineCourseManagementSystem.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string name, string message, int messageId, string creatorId, int room, DateTime createdOn, string userIconUrl)
        {
            await this.Clients.All.SendAsync("SendMessage", name, message, messageId, creatorId, room, createdOn, userIconUrl);
        }

        public async Task UpdateMessage(string newMessage, int messageId, int room)
        {
            await this.Clients.All.SendAsync("UpdateMessage", newMessage, messageId, room);
        }

        public async Task DeleteMessage(int messageId, int room)
        {
            await this.Clients.All.SendAsync("DeleteMessage", messageId, room);
        }

        public async Task OnInputMessage(string name, string userId, int room)
        {
            await this.Clients.All.SendAsync("OnInputMessage", name, userId, room);
        }
    }
}
