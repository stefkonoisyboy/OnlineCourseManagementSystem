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
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class FilesService : IFilesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<File> fileRepository;
        private readonly Cloudinary cloudinaryUtility;
        private readonly IDeletableEntityRepository<Album> albumRepository;
        private readonly CloudinaryService cloudinaryService;

        public FilesService(IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<File> fileRepository, Cloudinary cloudinaryUtility, IDeletableEntityRepository<Album> albumRepository)
        {
            this.userRepository = userRepository;
            this.fileRepository = fileRepository;

            this.cloudinaryUtility = cloudinaryUtility;
            this.albumRepository = albumRepository;

            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
        }

        public async Task<int?> DeleteImageFromGallery(int fileId, string userId)
        {
            File file = this.fileRepository.All().FirstOrDefault(f => f.Id == fileId);
            this.fileRepository.Delete(file);

            await this.fileRepository.SaveChangesAsync();

            return file.AlbumId;
        }

        public IEnumerable<T> GetAllById<T>(int lectureId, int id)
        {
            return this.fileRepository
                .All()
                .Where(f => f.LectureId == lectureId && f.Id != id)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllResourceFilesByAssignemt<T>(int assignmentId, string userId)
        {
            return this.fileRepository
                .All()
                .Where(f => f.AssignmentId == assignmentId && f.Type == FileType.Resource)
                .To<T>()
                .ToList();
        }

        public AllImagesViewModel GetAllImagesForUser(string userId, int albumId)
        {
            var images = this.fileRepository
                .All()
                .Where(x => x.UserId == userId && x.RemoteUrl.Contains("gallery") && x.AlbumId == albumId)
                .To<ImageViewModel>().ToArray();

            string albumName = this.albumRepository.All().FirstOrDefault(x => x.Id == albumId).Name;
            AllImagesViewModel allImages = new AllImagesViewModel
            {
                Images = images,
                AlbumId = albumId,
                AlbumName = albumName,
            };

            return allImages;
        }

        public async Task AddImages(UploadImageInputModel uploadImageInputModel)
        {
            ApplicationUser user = this.userRepository.All().FirstOrDefault(s => s.Id == uploadImageInputModel.UserId);

            if (uploadImageInputModel.Images == null)
            {
                return;
            }

            foreach (var image in uploadImageInputModel.Images)
            {
                string fileName = $"gallery_IMG{DateTime.UtcNow.ToString("yyyy/dd/mm/ss")}";
                string extension = System.IO.Path.GetExtension(image.FileName);
                File file = new File()
                {
                    Extension = extension,
                    UserId = uploadImageInputModel.UserId,
                    AlbumId = uploadImageInputModel.AlbumId,
                    RemoteUrl = await this.cloudinaryService.UploadFile(image, fileName, extension, "gallery"),
                };

                user.Files.Add(file);
            }

            await this.userRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllUserSubmittedFilesForAssignment<T>(int assignmentId, string userId)
        {
            return this.fileRepository
                .All()
                .Where(f => f.AssignmentId == assignmentId && f.UserId == userId && f.Type == FileType.Submit)
                .To<T>()
                .ToList();
        }

        public async Task<int?> DeleteWorkFileFromAssignment(int fileId)
        {
            File file = this.fileRepository
                .All()
                .FirstOrDefault(f => f.Id == fileId);

            this.fileRepository.Delete(file);

            await this.fileRepository.SaveChangesAsync();

            return file.AssignmentId;
        }

        public string GetRemoteUrlById(int id)
        {
            return this.fileRepository
                .All()
                .FirstOrDefault(f => f.Id == id)
                .RemoteUrl;
        }

        public T GetById<T>(int id)
        {
            return this.fileRepository
                .All()
                .Where(f => f.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task<int?> DeleteAsync(int id)
        {
            File file = this.fileRepository.All().FirstOrDefault(f => f.Id == id);
            int? lectureId = file.LectureId;
            this.fileRepository.Delete(file);
            await this.fileRepository.SaveChangesAsync();
            return lectureId;
        }
    }
}
