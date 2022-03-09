namespace OnlineCourseManagementSystem.Web.ViewModels.Channels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class GetAllChannelsByParticipantIdViewModel : IMapFrom<Channel>
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
