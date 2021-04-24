namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public interface IFilesService
    {
        Task AddImages(UploadImageInputModel uploadImageInputModel);

        AllImagesViewModel GetAllImagesForUser(string userId, int albumId);

        Task<int?> DeleteImageFromGallery(int fileId, string userId);

        IEnumerable<T> GetAllById<T>(int lectureId);

        IEnumerable<T> GetAllByUserAndAssignment<T>(int assignmentId, string userId);

        IEnumerable<T> GetAllUserSubmittedFilesForAssignment<T>(int assignmentId, string userId);
    }
}
