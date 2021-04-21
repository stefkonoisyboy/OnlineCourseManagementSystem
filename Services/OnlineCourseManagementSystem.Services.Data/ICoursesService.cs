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

        Task UpdateAsync(EditCourseInputModel input);

        Task DeleteAsync(int id);

        Task ApproveAsync(int courseId);

        Task EnrollAsync(int courseId, string userId);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllUnapproved<T>();

        T GetById<T>(int id);

        IEnumerable<T> GetAllByUser<T>(string userId);

        IEnumerable<T> GetAllUpcoming<T>();

        IEnumerable<T> GetAllPast<T>();

        IEnumerable<T> GetAllActive<T>();

        IEnumerable<T> GetAllByTag<T>(SearchByTagInputModel input);

        IEnumerable<SelectListItem> GetAllActive();
    }
}
