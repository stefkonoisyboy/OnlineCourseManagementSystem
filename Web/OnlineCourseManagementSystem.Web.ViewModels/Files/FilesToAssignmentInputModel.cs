namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class FilesToAssignmentInputModel : BaseFileInputModel
    {
        public int AssignmentId { get; set; }
    }
}
