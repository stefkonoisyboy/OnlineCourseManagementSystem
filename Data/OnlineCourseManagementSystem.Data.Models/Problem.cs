namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Problem : BaseDeletableModel<int>
    {
        public Problem()
        {
            this.Tests = new HashSet<Test>();
            this.Submissions = new HashSet<Submission>();
        }

        public string Name { get; set; }

        public int Points { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<Test> Tests { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
