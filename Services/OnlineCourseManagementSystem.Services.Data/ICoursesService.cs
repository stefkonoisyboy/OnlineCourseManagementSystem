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
        Task CreateAsync(CreateCourseInputModel input);

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

        IEnumerable<T> GetAllByAdmin<T>();
    }
}
