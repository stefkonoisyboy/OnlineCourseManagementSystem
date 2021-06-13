using OnlineCourseManagementSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Data.Models
{
    public class Channel : BaseDeletableModel<int>
    {
        public Channel()
        {
            this.AudienceComments = new HashSet<AudienceComment>();
            this.Users = new HashSet<UserChannel>();
        }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<AudienceComment> AudienceComments { get; set; }

        public virtual ICollection<UserChannel> Users { get; set; }
    }
}
