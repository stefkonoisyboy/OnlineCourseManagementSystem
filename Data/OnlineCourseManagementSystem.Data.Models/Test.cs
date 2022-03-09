namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Test : BaseDeletableModel<int>
    {
        public string Input { get; set; }

        public string Output { get; set; }

        public int ProblemId { get; set; }

        public virtual Problem Problem { get; set; }
    }
}
