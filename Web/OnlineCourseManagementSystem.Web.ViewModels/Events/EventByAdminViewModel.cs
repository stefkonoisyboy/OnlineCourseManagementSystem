namespace OnlineCourseManagementSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class EventByAdminViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Theme { get; set; }

        public string CreatorId { get; set; }

        public UserViewModel Creator { get; set; }

        public string Address { get; set; }

        public bool IsApproved { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
