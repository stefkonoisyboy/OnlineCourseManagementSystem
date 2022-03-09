namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;

    public interface ILecturesService
    {
        /// <summary>
        /// Create lecture.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateLectureInputModel input);

        /// <summary>
        /// Update lecture.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EditLectureInputModel input);

        /// <summary>
        /// Update modified on by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task UpdateModifiedOnById(int id);

        /// <summary>
        /// Delete lecture.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Add word file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddWordFileAsync(AddWordDocumentToLectureInputModel input);

        /// <summary>
        /// Add presentation file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddPresentationFileAsync(AddPresentationToLectureInputModel input);

        /// <summary>
        /// Add video.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddVideoAsync(AddVideoToLectureInputModel input);

        /// <summary>
        /// Get lectures count by creator id.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetLecturesCountByCreatorId(string creatorId);

        /// <summary>
        /// Get course id by lecture id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int GetCourseIdByLectureId(int id);

        /// <summary>
        /// Get lectures count by id.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        int GetLecturesCountById(int courseId);

        /// <summary>
        /// Get name by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetNameById(int id);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Get all by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllById<T>(int courseId, int page, int itemsPerPage = 3);

        /// <summary>
        /// Get all by file id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByFileId<T>(int id);

        /// <summary>
        /// Get all by creator id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="creatorId"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCreatorId<T>(int page, string creatorId, int items = 3);

        /// <summary>
        /// Get all videos by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllVideosById<T>(int lectureId);

        /// <summary>
        /// Get all in interval.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllInInterval<T>(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Get all by admin.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllByAdmin<T>();
    }
}
