using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    public class AddVideoToLectureInputModel
    {
        public string UserId { get; set; }

        public int LectureId { get; set; }

        [Required]
        public string RemoteUrl { get; set; }
    }
}
