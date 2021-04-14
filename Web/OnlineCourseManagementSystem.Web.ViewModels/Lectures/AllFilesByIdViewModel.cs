using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    public class AllFilesByIdViewModel : IMapFrom<File>
    {
        public string Extension { get; set; }

        public string RemoteUrl { get; set; }
    }
}
