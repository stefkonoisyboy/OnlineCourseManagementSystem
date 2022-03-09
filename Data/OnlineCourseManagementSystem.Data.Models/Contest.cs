namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Contest : BaseDeletableModel<int>
    {
        public Contest()
        {
            this.Problems = new HashSet<Problem>();
        }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Problem> Problems { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
