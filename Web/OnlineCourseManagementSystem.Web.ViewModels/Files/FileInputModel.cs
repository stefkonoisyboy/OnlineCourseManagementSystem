﻿namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class FileInputModel
    {
        public IEnumerable<IFormFile> Files { get; set; }

        public string UserId { get; set; }
    }
}
