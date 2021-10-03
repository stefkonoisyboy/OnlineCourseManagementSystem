namespace OnlineCourseManagementSystem.Services.Data
{
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

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
