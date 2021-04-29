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
        string GetFullNameById(string studentId);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllById<T>(string parentId);

        IEnumerable<T> GetAllByCourse<T>(int courseId);

        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        IEnumerable<SelectListItem> GetAllByParentAsSelectListItems(string parentId);

        Task AddParent(AddParentInputModel input);

        IEnumerable<SelectListItem> GetAllAsSelectListItems(int courseId);
    }
}
