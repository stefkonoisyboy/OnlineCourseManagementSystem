namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StripeSettings
    {
        public string SecretKey { get; set; }

        public string PublishableKey { get; set; }
    }
}
