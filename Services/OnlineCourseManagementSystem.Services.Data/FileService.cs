namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class FileService : IFileService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<File> fileRepository;
        private readonly Cloudinary cloudinaryUtility;

        public FileService(IDeletableEntityRepository<ApplicationUser> userRepository,IDeletableEntityRepository<File> fileRepository,Cloudinary cloudinaryUtility)
        {
            this.userRepository = userRepository;
            this.fileRepository = fileRepository;

            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task DeleteImageFromGallery(int fileId, string userId)
        {
            File file = this.fileRepository.All().FirstOrDefault(f => f.Id == fileId);
            this.fileRepository.Delete(file);

            await this.fileRepository.SaveChangesAsync();
        }

        public IEnumerable<ImageViewModel> GetAllImagesForUser(string userId)
        {
            var images = this.fileRepository
                .All()
                .Where(x => x.UserId == userId && x.RemoteUrl.Contains("gallery"))
                .To<ImageViewModel>().ToArray();

            return images;
        }

        [Obsolete]
        public async Task UploadImage(UploadImageInputModel uploadImageInputModel)
        {
            ApplicationUser user = this.userRepository.All().FirstOrDefault(s => s.Id == uploadImageInputModel.UserId);

            foreach (var image in uploadImageInputModel.Images)
            {
                string extension = image.ContentType;
                string fileName = $"Gallery_IMG{DateTime.UtcNow.ToString("yyyy/dd/mm/ss")}";
                byte[] destinationData;
                using (var ms = new System.IO.MemoryStream())
                {
                    await image.CopyToAsync(ms);
                    destinationData = ms.ToArray();
                }

                UploadResult uploadResult = null;

                using (var ms = new System.IO.MemoryStream(destinationData))
                {
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        Folder = "gallery",
                        File = new FileDescription(fileName, ms),
                    };

                    uploadResult = this.cloudinaryUtility.Upload(uploadParams);
                }

                string remoteUrl = uploadResult?.SecureUri.AbsoluteUri;

                File file = new File()
                {
                    Extension = extension,
                    UserId = uploadImageInputModel.UserId,
                    Album = uploadImageInputModel.Album,
                    RemoteUrl = remoteUrl,
                };

                user.Files.Add(file);
            }

            await this.userRepository.SaveChangesAsync();
        }


    }
}
