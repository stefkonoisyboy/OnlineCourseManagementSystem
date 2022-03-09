namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IParentsService
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// Get all as select list items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}
