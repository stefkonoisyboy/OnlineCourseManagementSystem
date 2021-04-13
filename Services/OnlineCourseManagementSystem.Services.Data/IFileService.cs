namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public interface IFileService
    {
        Task UploadImage(UploadImageInputModel uploadImageInputModel);

        AllImagesViewModel GetAllImagesForUser(string userId, int albumId);

        Task DeleteImageFromGallery(int fileId, string userId);

        IEnumerable<T> GetAllById<T>(int lectureId);
    }
}
