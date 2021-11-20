namespace OnlineCourseManagementSystem.Web.ViewModels.Subscribers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ConfirmSubscriptionInputModel : IMapFrom<Subscriber>
    {
        public string Id { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
