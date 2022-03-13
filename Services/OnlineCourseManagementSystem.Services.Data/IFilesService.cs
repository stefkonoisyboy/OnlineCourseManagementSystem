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
        /// <summary>
        /// This method adds file as image to album of user.
        /// </summary>
        /// <param name="uploadImageInputModel"></param>
        /// <returns></returns>
        Task AddImages(UploadImageInputModel uploadImageInputModel);

        /// <summary>
        /// This method gets all files for album of user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <returns></returns>
        T GetAllImagesForUserByAlbum<T>(string userId, int albumId);

        /// <summary>
        /// This method deletes file from album of user.
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int?> DeleteImageFromGallery(int fileId, string userId);

        /// <summary>
        /// This method delets file for lecture and returns lectureId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int?> DeleteAsync(int id);

        /// <summary>
        /// This method gets file remoteurl.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetRemoteUrlById(int id);

        /// <summary>
        /// This method gets file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// This method gets all files for lecture skipping certain file by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lectureId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllById<T>(int lectureId, int id);

        /// <summary>
        /// This method gets all resource files for assignment of user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assignmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllResourceFilesByAssignemt<T>(int assignmentId, string userId);

        /// <summary>
        /// This method gets all submitted files for assignment of user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assignmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllUserSubmittedFilesForAssignment<T>(int assignmentId, string userId);

        /// <summary>
        /// This method deletes wor file from assignment
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<int?> DeleteWorkFileFromAssignment(int fileId);

        /// <summary>
        /// This method delets file from event.
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<int?> DeleteFromEventAsync(int fileId);

        /// <summary>
        /// This method adds url as video file to event.
        /// </summary>
        /// <param name="inptuModel"></param>
        /// <returns></returns>
        Task<int> AddVideoResourceToEventAsync(VideoFileInputModel inptuModel);

        Task MarkAsCompletedAsync(int id, string userId);

        /// <summary>
        /// Adds file to album.
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        Task AddToAlbumFromLecture(int fileId, int lectureId);
    }
}
