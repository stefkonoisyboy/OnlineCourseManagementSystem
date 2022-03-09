namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using OnlineCourseManagementSystem.Data.Models;

    public class CloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;
        private const string DEAFULT_FOLDER_NAME = "files";

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadFile(IFormFile file, string fileName, string extension, string folder = DEAFULT_FOLDER_NAME)
        {
            byte[] destinationData;

            using (var ms = new System.IO.MemoryStream())
            {
                await file.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            UploadResult result = null;

            if (extension != ".pptx" && extension != ".docx" && extension != ".pdf")
            {
                using (var ms = new System.IO.MemoryStream(destinationData))
                {
                    ImageUploadParams uploadParams = new ImageUploadParams
                    {
                        Folder = folder,
                        File = new FileDescription(fileName, ms),
                    };

                    result = this.cloudinaryUtility.Upload(uploadParams);
                }
            }
            else
            {
                using (var ms = new System.IO.MemoryStream(destinationData))
                {
                    RawUploadParams uploadParams = new RawUploadParams
                    {
                        Folder = folder,
                        File = new FileDescription(fileName, ms),
                        PublicId = fileName + extension,
                    };

                    result = this.cloudinaryUtility.Upload(uploadParams);
                }
            }

            return result?.SecureUrl.AbsoluteUri;
        }
    }
}
