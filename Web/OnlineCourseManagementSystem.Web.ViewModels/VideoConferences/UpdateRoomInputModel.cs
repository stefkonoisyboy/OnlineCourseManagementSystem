namespace OnlineCourseManagementSystem.Web.ViewModels.VideoConferences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class UpdateRoomInputModel : IMapFrom<Room>
    {
        public string Id { get; set; }

        public bool IsDisplayCameraAllowed { get; set; }

        public bool IsUnmuteMicAllowed { get; set; }

        public bool IsShareScreenAllowed { get; set; }
    }
}
