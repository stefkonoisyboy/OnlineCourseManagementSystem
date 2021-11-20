namespace OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class FileExtensionAttribute : ValidationAttribute
    {
        public FileExtensionAttribute()
        {
            this.ErrorMessage = "Invalid File Format!";
        }

        public override bool IsValid(object value)
        {
            string[] allowedExtensions = new string[] { ".pptx", ".docx", ".pdf" };

            if (value is IFormFile file)
            {
                string extension = Path.GetExtension(file.FileName);

                if (!allowedExtensions.Contains(extension))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
