namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Albums;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public interface IAlbumsService
    {

        Task CreateAsync(AlbumInputModel albumInputModel);

        IEnumerable<T> GetAllById<T>(string userId);

        Task UpdateAsync(string userId, int albumId, string name);
    }
}
