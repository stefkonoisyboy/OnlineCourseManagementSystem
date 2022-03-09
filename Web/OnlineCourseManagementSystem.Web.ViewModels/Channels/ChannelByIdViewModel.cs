namespace OnlineCourseManagementSystem.Web.ViewModels.Channels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ChannelByIdViewModel : IMapFrom<Channel>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
