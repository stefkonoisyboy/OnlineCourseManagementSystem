using OnlineCourseManagementSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Data.Models
{
    public class Room : BaseDeletableModel<string>
    {
        public Room()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public bool IsDisplayCameraAllowed { get; set; }

        public bool IsUnmutedMicAllowed { get; set; }

        public bool IsShareScreenAllowed { get; set; }

        public virtual ICollection<RoomParticipant> ParticipantsInRooms { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsActive { get; set; }
    }
}
