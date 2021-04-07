namespace OnlineCourseManagementSystem.Services.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.ViewModels.Students;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IStudentsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllById<T>(string parentId);

        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        Task AddParent(AddParentInputModel input);
    }
}
