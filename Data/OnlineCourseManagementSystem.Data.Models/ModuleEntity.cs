namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class ModuleEntity : BaseDeletableModel<int>
    {
        public ModuleEntity()
        {
            this.Subjects = new HashSet<Subject>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<TrainingModule> Trainings { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
