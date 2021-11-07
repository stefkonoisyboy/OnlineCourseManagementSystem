namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Events;

    public class EventVideoByIdViewModel : IMapFrom<File>
    {
        public int Id { get; set; }

        public string RemoteUrl { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
