namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class FileAssignmentViewModel : IMapFrom<File>
    {
        public string RemoteUrl { get; set; }
    }
}
