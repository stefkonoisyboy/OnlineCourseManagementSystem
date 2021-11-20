namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Subscriber : BaseDeletableModel<string>
    {
        public Subscriber()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Email { get; set; }

        public DateTime? ConfirmedDate { get; set; }

        public bool? IsConfirmed { get; set; }
    }
}
