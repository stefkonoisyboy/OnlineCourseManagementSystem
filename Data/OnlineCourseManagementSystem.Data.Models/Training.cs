namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class Training : BaseDeletableModel<int>
    {
        public Training()
        {
            this.Modules = new HashSet<TrainingModule>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageRemoteUrl { get; set; }

        public TrainingType TrainingType { get; set; }

        public virtual ICollection<TrainingModule> Modules { get; set; }
    }
}
