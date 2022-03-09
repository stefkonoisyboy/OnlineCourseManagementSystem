namespace OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class PresentationExtensionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is IFormFile formFile)
            {
                string extension = Path.GetExtension(formFile.FileName);

                if (extension == ".pptx")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
