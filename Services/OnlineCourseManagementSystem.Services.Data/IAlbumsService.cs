using OnlineCourseManagementSystem.Web.ViewModels.Albums;
using OnlineCourseManagementSystem.Web.ViewModels.Files;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IAlbumsService
    {

        Task CreateAsync(BaseAlbumInputModel albumInputModel);

        IEnumerable<T> GetAllById<T>(string userId);

        Task UpdateAsync(string userId, int albumId, string name);
    }
}
