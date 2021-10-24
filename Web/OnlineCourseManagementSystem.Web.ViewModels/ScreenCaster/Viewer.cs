namespace OnlineCourseManagementSystem.Web.ViewModels.ScreenCaster
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Viewer
    {
        public Viewer(string connectionId, string agentName)
        {
            this.ConnectionId = connectionId;
            this.AgentName = agentName;
        }

        public string ConnectionId { get; set; }

        public string AgentName { get; set; }
    }
}
