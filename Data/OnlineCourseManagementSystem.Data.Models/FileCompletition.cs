namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class FileCompletition : BaseDeletableModel<int>
    {
        public int FileId { get; set; }

        public virtual File File { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
