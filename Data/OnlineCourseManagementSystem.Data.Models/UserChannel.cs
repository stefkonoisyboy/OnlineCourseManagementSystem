using OnlineCourseManagementSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Data.Models
{
    public class UserChannel : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ChannelId { get; set; }

        public virtual Channel Channel { get; set; }
    }
}
