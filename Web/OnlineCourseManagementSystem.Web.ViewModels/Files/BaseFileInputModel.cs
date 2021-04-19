using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    public class BaseFileInputModel
    {
        public IEnumerable<IFormFile> Files { get; set; }

        public string UserId { get; set; }

    }
}
