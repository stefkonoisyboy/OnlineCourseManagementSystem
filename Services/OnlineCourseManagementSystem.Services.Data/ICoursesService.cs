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

        /// <summary>
        /// Update course.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EditCourseInputModel input);

        /// <summary>
        /// Update course meta.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateMetaAsync(EditMetaInputModel input);

        /// <summary>
        /// Update course modified on.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task UpdateModifiedOnById(int id);

        /// <summary>
        /// Delete course.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Approve course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task ApproveAsync(int courseId);

        /// <summary>
        /// Enroll in course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task EnrollAsync(int courseId, string userId, string path);

        /// <summary>
        /// Get course name by student and course.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        string CourseNameByStudentAndCourse(string studentId, int courseId);

        /// <summary>
        /// Get all active courses count.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetAllActiveCoursesCount(string name);

        /// <summary>
        /// Check if course is available.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsCourseAvailable(int courseId, string userId);

        /// <summary>
        /// Get all active courses by subject id count.
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        int GetAllActiveCoursesBySubjectIdCount(int subjectId);

        /// <summary>
        /// Get all courses by user id and subject id count.
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetAllCoursesByUserIdAndSubjectIdCount(int subjectId, string userId);

        /// <summary>
        /// Get all active courses by creator id and subject id count.
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetAllCoursesByCreatorIdAndSubjectIdCount(int subjectId, string creatorId);

        /// <summary>
        /// Get all courses by user id count.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetAllCoursesByUserIdCount(string userId);

        /// <summary>
        /// Get all courses by creator id count.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetAllCoursesByCreatorIdCount(string creatorId);

        /// <summary>
        /// Get course id by file id.
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        int GetCourseIdByFileId(int fileId);

        /// <summary>
        /// Get all for testing AI by user id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<UserInCourse> GetAllForTestingAIByUserId(int id, string userId, int itemsPerPage = 12);

        /// <summary>
        /// Get all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// Get all unapproved.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllUnapproved<T>();

        /// <summary>
        /// Get all completed by user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllCompletedByUserId<T>(string userId);

        /// <summary>
        /// Get all followed by user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllFollewedByUserId<T>(string userId);

        /// <summary>
        /// Get all by name or tag.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByNameOrTag<T>(SearchByCourseNameOrTagInputModel input);

        /// <summary>
        /// Get all recommended.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllRecommended<T>();

        /// <summary>
        /// Get top latest.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetTopLatest<T>();

        /// <summary>
        /// Get top next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetTopNext<T>();

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Get all by user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByUser<T>(int id, string userId, int itemsPerPage = 6);

        /// <summary>
        /// Get all by user and subject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="subjectId"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByUserAndSubject<T>(int id, string userId, int subjectId, int itemsPerPage = 6);

        /// <summary>
        /// Get all by creator id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCreatorId<T>(int id, string creatorId, int itemsPerPage = 6);

        /// <summary>
        /// Get all by creator id and subject id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="subjectId"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCreatorIdAndSubjectId<T>(int id, string creatorId, int subjectId, int itemsPerPage = 6);

        /// <summary>
        /// Get all upcoming.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllUpcoming<T>();

        /// <summary>
        /// Get all past.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllPast<T>();

        /// <summary>
        /// Get all active.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="name"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllActive<T>(int page, string name, int itemsPerPage = 6);

        /// <summary>
        /// Get all active by subject id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="subjectId"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllActiveBySubjectId<T>(int page, int subjectId, int itemsPerPage = 6);

        /// <summary>
        /// Get all by tag.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByTag<T>(SearchByTagInputModel input);

        /// <summary>
        /// Get all active.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllActive();

        /// <summary>
        /// Get all as select list items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        /// <summary>
        /// Get all as select list items by creator id.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>

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

        /// <summary>
        /// Gets all top latest courses by subject in module.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllTopLatestBySubjectInModule<T>(int moduleId);
    }
}
