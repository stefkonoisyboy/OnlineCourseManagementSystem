namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUsersService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);

        string GetFullNameById(string id);

        string GetProfileImageUrlById(string id);
    }
}
