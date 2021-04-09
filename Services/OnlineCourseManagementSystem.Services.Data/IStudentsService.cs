namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.ViewModels.Students;

    public interface IStudentsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllById<T>(string parentId);

        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        Task AddParent(AddParentInputModel input);
    }
}
