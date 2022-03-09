namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ITownsService
    {
        /// <summary>
        /// Get all as select list items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}
