namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data.MachineLearning;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;

    public interface ICoursesService
    {
        /// <summary>
        /// This method creates course.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateCourseInputModel input);

        /// <summary>
        /// This method creates meta for course.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateMetaAsync(CreateMetaInputModel input);

        Task UpdateAsync(EditCourseInputModel input);

        Task UpdateMetaAsync(EditMetaInputModel input);

        Task UpdateModifiedOnById(int id);

        Task DeleteAsync(int id);

        Task ApproveAsync(int courseId);

        Task EnrollAsync(int courseId, string userId, string path);

        string CourseNameByStudentAndCourse(string studentId, int courseId);

        int GetAllActiveCoursesCount(string name);

        bool IsCourseAvailable(int courseId, string userId);

        int GetAllActiveCoursesBySubjectIdCount(int subjectId);

        int GetAllCoursesByUserIdAndSubjectIdCount(int subjectId, string userId);

        int GetAllCoursesByCreatorIdAndSubjectIdCount(int subjectId, string creatorId);

        int GetAllCoursesByUserIdCount(string userId);

        int GetAllCoursesByCreatorIdCount(string creatorId);

        int GetCourseIdByFileId(int fileId);

        IEnumerable<UserInCourse> GetAllForTestingAIByUserId(int id, string userId, int itemsPerPage = 12);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllUnapproved<T>();

        IEnumerable<T> GetAllCompletedByUserId<T>(string userId);

        IEnumerable<T> GetAllFollewedByUserId<T>(string userId);

        IEnumerable<T> GetAllByNameOrTag<T>(SearchByCourseNameOrTagInputModel input);

        IEnumerable<T> GetAllRecommended<T>();

        IEnumerable<T> GetTopLatest<T>();

        IEnumerable<T> GetTopNext<T>();

        T GetById<T>(int id);

        IEnumerable<T> GetAllByUser<T>(int id, string userId, int itemsPerPage = 6);

        IEnumerable<T> GetAllByUserAndSubject<T>(int id, string userId, int subjectId, int itemsPerPage = 6);

        IEnumerable<T> GetAllByCreatorId<T>(int id, string creatorId, int itemsPerPage = 6);

        IEnumerable<T> GetAllByCreatorIdAndSubjectId<T>(int id, string creatorId, int subjectId, int itemsPerPage = 6);

        IEnumerable<T> GetAllUpcoming<T>();

        IEnumerable<T> GetAllPast<T>();

        IEnumerable<T> GetAllActive<T>(int page, string name, int itemsPerPage = 6);

        IEnumerable<T> GetAllActiveBySubjectId<T>(int page, int subjectId, int itemsPerPage = 6);

        IEnumerable<T> GetAllByTag<T>(SearchByTagInputModel input);

        IEnumerable<SelectListItem> GetAllActive();

        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        IEnumerable<SelectListItem> GetAllAsSelectListItemsByCreatorId(string creatorId);

        /// <summary>
        /// This method gets all courses for admin.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllByAdmin<T>();

        /// <summary>
        /// This method gets lastactive course or still active course by subject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        T GetLastActiveCourseBySubjectId<T>(int subjectId);

        /// <summary>
        /// This method gets all courses by subject for current year.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllCurrentYearBySubjectId<T>(int subjectId);

        /// <summary>
        /// This method gets all courses by subject and course name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjectId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<T> GetBySubjectAndCourseName<T>(int subjectId, string name);

        /// <summary>
        /// This method gets all unactive(that have finished) courses by subjectId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllUnactiveCourses<T>(int subjectId);
    }
}
