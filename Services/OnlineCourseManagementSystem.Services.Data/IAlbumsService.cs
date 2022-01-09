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
        /// <summary>
        /// This method creates new album for user.
        /// </summary>
        /// <param name="albumInputModel"></param>
        /// <returns></returns>
        Task CreateAsync(AlbumInputModel albumInputModel);

        /// <summary>
        /// This method gets all albums of user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllById<T>(string userId);

        /// <summary>
        /// This method updates album's name.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task UpdateAsync(EditAlbumInputModel inputModel);
    }
}
