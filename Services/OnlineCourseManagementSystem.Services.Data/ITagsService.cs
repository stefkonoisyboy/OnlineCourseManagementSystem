namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ITagsService
    {
        /// <summary>
        /// Get all as select list items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        /// <summary>
        /// Get all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// Get all by course id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCourseId<T>(int courseId);
    }
}
