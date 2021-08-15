namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class UploadImageInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }

        public int AlbumId { get; set; }

        public string UserId { get; set; }
    }
}
