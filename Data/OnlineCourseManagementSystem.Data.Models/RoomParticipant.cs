namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class RoomParticipant : BaseDeletableModel<string>
    {
        public RoomParticipant()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string RoomId { get; set; }

        public Room Room { get; set; }

        public bool IsGivenRightToDisplayCamera { get; set; }

        public bool IsGivenRightToUnmuteMic { get; set; }

        public bool IsGivenRightToShareScreen { get; set; }

        public bool IsCreator { get; set; }
    }
}
