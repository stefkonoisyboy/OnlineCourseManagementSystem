namespace OnlineCourseManagementSystem.Web.ViewModels.Submissions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class Top5SubmissionsByUserIdAndContestIdViewModel : IMapFrom<Submission>
    {
        public int Id { get; set; }

        public int Points { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProblemName { get; set; }

        public int ProblemPoints { get; set; }
    }
}
