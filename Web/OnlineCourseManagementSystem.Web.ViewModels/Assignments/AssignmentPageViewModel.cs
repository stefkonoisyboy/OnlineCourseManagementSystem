namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class AssignmentPageViewModel
    {
        public string Instructions { get; set; }

        public int PossiblePoints { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }
    }
}
