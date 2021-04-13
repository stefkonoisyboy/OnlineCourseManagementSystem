using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    public class AssignmentPageViewModel
    {
        public string Instructions { get; set; }

        public int PossiblePoints { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }
    }
}
