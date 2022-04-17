namespace OnlineCourseManagementSystem.Web.ViewModels.VideoConferences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ParticipantViewModel : IMapFrom<RoomParticipant>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string RoomId { get; set; }

        public bool IsGivenRightToDisplayCamera { get; set; }

        public bool IsGivenRightToUnmuteMic { get; set; }

        public bool IsGivenRightToShareScreen { get; set; }
    }
}
