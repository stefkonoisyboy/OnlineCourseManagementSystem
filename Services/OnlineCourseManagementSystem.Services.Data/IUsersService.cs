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

        T GetById<T>(string id);

        string GetFullNameById(string id);

        string GetProfileImageUrlById(string id);

        Task UpdateAsync(ManageAccountInputModel inputModel);

        //T GetCourseCreatorById<T>(int courseId);
    }
}
