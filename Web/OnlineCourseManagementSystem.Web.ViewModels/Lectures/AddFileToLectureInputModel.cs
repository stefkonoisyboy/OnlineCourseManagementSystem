namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class AddFileToLectureInputModel
    {
        public string UserId { get; set; }

        public int LectureId { get; set; }

        public IFormFile File { get; set; }
    }
}
