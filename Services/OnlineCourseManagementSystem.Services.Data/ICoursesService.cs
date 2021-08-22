namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
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

        Task EnrollAsync(int courseId, string userId);

        string CourseNameByStudentAndCourse(string studentId, int courseId);

        int GetAllActiveCoursesCount(string name);

        int GetAllCoursesByUserIdCount(string userId);

        int GetAllCoursesByCreatorIdCount(string creatorId);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllUnapproved<T>();

        IEnumerable<T> GetAllByNameOrTag<T>(SearchByCourseNameOrTagInputModel input);

        IEnumerable<T> GetAllRecommended<T>();

        T GetById<T>(int id);

        IEnumerable<T> GetAllByUser<T>(int id, string userId, int itemsPerPage = 6);

        IEnumerable<T> GetAllByCreatorId<T>(int id, string creatorId, int itemsPerPage = 6);

        IEnumerable<T> GetAllUpcoming<T>();

        IEnumerable<T> GetAllPast<T>();

        IEnumerable<T> GetAllActive<T>(int page, string name, int itemsPerPage = 5);

        IEnumerable<T> GetAllByTag<T>(SearchByTagInputModel input);

        IEnumerable<SelectListItem> GetAllActive();

        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        IEnumerable<SelectListItem> GetAllAsSelectListItemsByCreatorId(string creatorId);
    }
}
