namespace OnlineCourseManagementSystem.Web.ViewModels.VideoConferences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RoomDetails
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int ParticipantCount { get; set; }

        public int MaxParticipants { get; set; }
    }
}
