namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class TrainingModule : BaseDeletableModel<int>
    {
        public virtual Training Training { get; set; }

        public int TrainingId { get; set; }

        public virtual ModuleEntity Module { get; set; }

        public int ModuleId { get; set; }
    }
}
