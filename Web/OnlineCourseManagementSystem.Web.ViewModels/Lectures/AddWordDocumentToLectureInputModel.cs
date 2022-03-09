namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class AddWordDocumentToLectureInputModel : AddBaseFileToLectureInputModel
    {
        [WordDocumentExtension(ErrorMessage = "The provided file is not a word document!")]
        public IFormFile File { get; set; }
    }
}
