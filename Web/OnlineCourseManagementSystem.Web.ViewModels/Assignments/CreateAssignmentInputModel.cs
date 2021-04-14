﻿namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateAssignmentInputModel
    {
        public string Instructions { get; set; }

        public int CourseId { get; set; }

        public IEnumerable<string> StudentsId { get; set; }

        public IEnumerable<SelectListItem> Students { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }

        public int PossiblePoints { get; set; }
    }
}
