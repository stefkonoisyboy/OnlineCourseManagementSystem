namespace OnlineCourseManagementSystem.Web.ViewModels.ContactMessages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MarkContactMessageAsSeenInputModel
    {
        public int ContactMessageId { get; set; }

        public string UserId { get; set; }
    }
}
