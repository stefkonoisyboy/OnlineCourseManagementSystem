namespace OnlineCourseManagementSystem.Web.ViewModels.Mails
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConfirmMailSubscriberRequest
    {
        public string ToEmail { get; set; }

        public string SubscriberId { get; set; }
    }
}
