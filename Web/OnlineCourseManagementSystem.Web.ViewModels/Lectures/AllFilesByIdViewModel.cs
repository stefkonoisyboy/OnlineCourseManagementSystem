namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllFilesByIdViewModel : IMapFrom<File>
    {
        public string Extension { get; set; }

        public string RemoteUrl { get; set; }
    }
}
