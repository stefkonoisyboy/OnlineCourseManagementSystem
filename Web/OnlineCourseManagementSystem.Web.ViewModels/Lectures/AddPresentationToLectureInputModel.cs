namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class AddPresentationToLectureInputModel : AddBaseFileToLectureInputModel
    {
        [PresentationExtension(ErrorMessage = "The provided file is not a presentation!")]
        public IFormFile File { get; set; }
    }
}
