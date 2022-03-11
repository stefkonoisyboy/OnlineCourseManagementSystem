using OnlineCourseManagementSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Data.Models
{
    public class ExecutedTest : BaseDeletableModel<int>
    {
        public string TestInput { get; set; }

        public string UserOutput { get; set; }

        public string ExpectedOutput { get; set; }

        public int SubmissionId { get; set; }

        public virtual Submission Submission { get; set; }

        public bool HasPassed { get; set; }
    }
}
