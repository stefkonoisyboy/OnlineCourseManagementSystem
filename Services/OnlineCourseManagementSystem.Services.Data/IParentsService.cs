namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IParentsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}
