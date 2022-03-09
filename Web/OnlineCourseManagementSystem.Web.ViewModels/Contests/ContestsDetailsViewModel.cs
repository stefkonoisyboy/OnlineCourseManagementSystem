namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Submissions;

    public class ContestsDetailsViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [IgnoreMap]
        public IEnumerable<Top5SubmissionsByUserIdAndContestIdViewModel> Submissions { get; set; }
    }
}
