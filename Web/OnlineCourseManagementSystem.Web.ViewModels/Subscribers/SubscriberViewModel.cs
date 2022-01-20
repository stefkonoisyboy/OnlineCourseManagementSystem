namespace OnlineCourseManagementSystem.Web.ViewModels.Subscribers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class SubscriberViewModel : IMapFrom<Subscriber>
    {
        public string Id { get; set; }

        public string Email { get; set; }
    }
}
