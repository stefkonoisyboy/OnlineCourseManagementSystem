﻿namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class UserAssignment : BaseDeletableModel<int>
    {
        public int AssignmentId { get; set; }

        public virtual Assignment Assignment { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}