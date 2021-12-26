namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ILecturersService
    {
        /// <summary>
        /// This method gets all lecturers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// This method gets all lecturers by courseId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllById<T>(int courseId);

        /// <summary>
        /// This method gets all lecturers by courseId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <returns></returns>
        IEnumerable<T> GetAllByCourseId<T>(int courseId);

        /// <summary>
        /// This method gets all lecturers as selectListItems.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        /// <summary>
        /// This method gets all lecturers by courseId as selectListItems.
        /// </summary>
        /// <param name="courseId"></param>
        IEnumerable<SelectListItem> GetAllByCourseAsSelectListItems(int courseId);
    }
}
