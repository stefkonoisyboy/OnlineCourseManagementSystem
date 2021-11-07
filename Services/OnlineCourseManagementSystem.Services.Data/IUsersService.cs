namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public interface IUsersService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetTop3ByCourseId<T>(int courseId);

        IEnumerable<T> GetTop4Teachers<T>();

        IEnumerable<T> GetTop4Students<T>();

        T GetById<T>(string id);

        string GetFullNameById(string id);

        string GetProfileImageUrlById(string id);

        Task UpdateAsync(ManageAccountInputModel inputModel);

        Task DeleteAsync(string userId);

        IEnumerable<T> GetAllStudents<T>();

        IEnumerable<T> GetAllLecturers<T>();

        //T GetCourseCreatorById<T>(int courseId);
    }
}
