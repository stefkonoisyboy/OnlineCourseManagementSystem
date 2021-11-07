namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class VideoFileInputModel
    {
        [Required]
        public string RemoteUrl { get; set; }

        public int EventId { get; set; }
    }
}
