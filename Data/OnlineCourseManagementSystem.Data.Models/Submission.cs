namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Submission : BaseDeletableModel<int>
    {
        public Submission()
        {
            this.ExecutedTests = new HashSet<ExecutedTest>();
        }

        public string Code { get; set; }

        public int Points { get; set; }

        public int ProblemId { get; set; }

        public virtual Problem Problem { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ExecutedTest> ExecutedTests { get; set; }
    }
}
