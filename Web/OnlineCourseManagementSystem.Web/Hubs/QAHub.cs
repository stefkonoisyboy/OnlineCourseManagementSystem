namespace OnlineCourseManagementSystem.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public class QAHub : Hub
    {
        public async Task SendMessage()
        {
            await this.Clients.All.SendAsync("SendMessage");
        }

        public async Task UpdateMessage()
        {
            await this.Clients.All.SendAsync("UpdateMessage");
        }

        public async Task WithdrawMessage()
        {
            await this.Clients.All.SendAsync("WithdrawMessage");
        }

        public async Task DeleteMessage()
        {
            await this.Clients.All.SendAsync("DeleteMessage");
        }

        public async Task SendReply()
        {
            await this.Clients.All.SendAsync("SendReply");
        }

        public async Task ChangeStarredStatus()
        {
            await this.Clients.All.SendAsync("ChangeStarredStatus");
        }

        public async Task ChangeHighlightedStatus()
        {
            await this.Clients.All.SendAsync("ChangeHighlightedStatus");
        }

        public async Task ChangeAnsweredStatus()
        {
            await this.Clients.All.SendAsync("ChangeAnsweredStatus");
        }

        public async Task LikeMessage()
        {
            await this.Clients.All.SendAsync("LikeMessage");
        }

        public async Task ArchiveMessage()
        {
            await this.Clients.All.SendAsync("ArchiveMessage");
        }

        public async Task ArchiveAllMessages()
        {
            await this.Clients.All.SendAsync("ArchiveAllMessages");
        }

        public async Task RestoreMessage()
        {
            await this.Clients.All.SendAsync("RestoreMessage");
        }

        public async Task RestoreAllMessages()
        {
            await this.Clients.All.SendAsync("RestoreAllMessages");
        }
    }
}
