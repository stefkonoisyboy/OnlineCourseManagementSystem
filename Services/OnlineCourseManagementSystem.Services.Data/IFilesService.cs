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

        T GetAllImagesForUserByAlbum<T>(string userId, int albumId);

        Task<int?> DeleteImageFromGallery(int fileId, string userId);

        Task<int?> DeleteAsync(int id);

        string GetRemoteUrlById(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAllById<T>(int lectureId, int id);

        IEnumerable<T> GetAllResourceFilesByAssignemt<T>(int assignmentId, string userId);

        IEnumerable<T> GetAllUserSubmittedFilesForAssignment<T>(int assignmentId, string userId);

        Task<int?> DeleteWorkFileFromAssignment(int fileId);

        Task<int?> DeleteFromEventAsync(int fileId);

        Task<int> AddVideoResourceToEventAsync(VideoFileInputModel inptuModel);
    }
}
