namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Events;

    public interface IEventsService
    {
        Task CreateAsync(CreateEventInputModel inputModel);

        Task DeleteAsync(int eventId);

        Task UpdateAsync(EditEventInputModel inputModel);

        /// <summary>
        /// This method gets all events for user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllCreatedByUserId<T>(string userId);

        /// <summary>
        /// Thsi methdo gets all events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// This method gets event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventId"></param>
        /// <returns></returns>
        T GetById<T>(int eventId);

        Task ApproveAsync(int eventId);

        Task DisapproveAsync(int eventId);

        /// <summary>
        /// This methdo gets all events that are coming soon.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllComing<T>();

        /// <summary>
        /// This method gets all events that finished.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllFinished<T>();

        /// <summary>
        /// This method gets all events by admin.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllByAdmin<T>();
    }
}
