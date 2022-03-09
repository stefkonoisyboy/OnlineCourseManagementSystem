namespace OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class CheckFileExtensionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string[] allowedExtensions = new string[] { ".pptx", ".docx", ".pdf", ".jpg", ".png" };
            var files = new[] { value };
            bool[] checkedTrue = new bool[files.Length];
            int i = 0;
            foreach (var file in files)
            {
                string fileExtension = string.Empty;
                if (file is IFormFile result)
                {
                    fileExtension = Path.GetExtension(result.FileName);
                }

                if (allowedExtensions.Contains(fileExtension))
                {
                    return false;
                }
                else
                {
                    checkedTrue[i] = true;
                }

                i++;
            }

            if (checkedTrue.Where(c => c).Count() == files.Length)
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
