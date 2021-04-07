namespace OnlineCourseManagementSystem.Services.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IParentsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}
