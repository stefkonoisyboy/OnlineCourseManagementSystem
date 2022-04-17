namespace OnlineCourseManagementSystem.Web.ViewModels.VideoConferences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateRoomInputModel
    {
        public string Name { get; set; }

        public string CreatorId { get; set; }

        public bool IsDisplayCameraAllowed { get; set; }

        public bool IsUnmutedMicAllowed { get; set; }

        public bool IsShareScreenAllowed { get; set; }
    }
}
