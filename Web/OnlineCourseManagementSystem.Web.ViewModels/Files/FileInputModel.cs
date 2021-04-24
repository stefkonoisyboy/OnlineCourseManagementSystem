namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class FileInputModel
    {
        [FileExtension]
        public IEnumerable<IFormFile> Files { get; set; }

        public string UserId { get; set; }
    }
}
