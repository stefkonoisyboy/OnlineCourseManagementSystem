namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using OnlineCourseManagementSystem.Data.Models;

    public class CloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        //public async Task AttachFile(UploadFileInputModel uploadImageInputModel)
        //{
        //    ApplicationUser user = this.userRepository.All().FirstOrDefault(s => s.Id == uploadImageInputModel.UserId);

        //    foreach (var image in uploadImageInputModel.Images)
        //    {
        //        string extension = image.ContentType;
        //        string fileName = $"Gallery_IMG{DateTime.UtcNow.ToString("yyyy/dd/mm/ss")}";
        //        byte[] destinationData;
        //        using (var ms = new System.IO.MemoryStream())
        //        {
        //            await image.CopyToAsync(ms);
        //            destinationData = ms.ToArray();
        //        }

        //        UploadResult uploadResult = null;

        //        using (var ms = new System.IO.MemoryStream(destinationData))
        //        {
        //            ImageUploadParams uploadParams = new ImageUploadParams()
        //            {
        //                Folder = "gallery",
        //                File = new FileDescription(fileName, ms),
        //            };

        //            uploadResult = this.cloudinaryUtility.Upload(uploadParams);
        //        }

        //        string remoteUrl = uploadResult?.SecureUri.AbsoluteUri;

        //        File file = new File()
        //        {
        //            Extension = extension,
        //            UserId = uploadImageInputModel.UserId,
        //            AlbumId = uploadImageInputModel.AlbumId,
        //            RemoteUrl = remoteUrl,
        //        };

        //        user.Files.Add(file);
        //    }

        //    await this.userRepository.SaveChangesAsync();
        //}
    }
}
