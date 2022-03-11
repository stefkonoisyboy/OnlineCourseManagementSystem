using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.ExecutedTests
{
    public class AllExecutedTestsBySubmissionIdViewModel : IMapFrom<ExecutedTest>
    {
        public int Id { get; set; }

        public int SubmissionId { get; set; }

        public string SubmissionCode { get; set; }

        public int SubmissionProblemId { get; set; }

        public int SubmissionContestId { get; set; }

        public string SubmissionUserUserName { get; set; }

        public string SubmissionProblemName { get; set; }

        public bool HasPassed { get; set; }

        public string TestInput { get; set; }

        public string UserOutput { get; set; }

        public string ExpectedOutput { get; set; }
    }
}
