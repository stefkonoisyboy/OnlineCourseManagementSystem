namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AssignmentUserInfoViewModel : IMapFrom<UserAssignment>
    {
        public string UserName { get; set; }

        public bool Seen { get; set; }

        public DateTime TurnedOn { get; set; }

        public bool Turned { get; set; }

        public IEnumerable<string> FilesUrl { get; set; }
    }
}
