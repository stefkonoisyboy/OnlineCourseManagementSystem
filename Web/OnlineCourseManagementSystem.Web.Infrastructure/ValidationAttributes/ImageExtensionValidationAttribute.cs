namespace OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class ImageExtensionValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string[] allowedExtensions = new string[] { ".jpg", ".png" };

            if (value is IFormFile image)
            {
                string extension = Path.GetExtension(image.FileName);

                if (!allowedExtensions.Contains(extension))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (value is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
