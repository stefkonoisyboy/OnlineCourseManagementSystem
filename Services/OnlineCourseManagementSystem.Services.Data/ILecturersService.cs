namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ILecturersService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllById<T>(int courseId);

        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        IEnumerable<SelectListItem> GetAllByCourseAsSelectListItems(int courseId);
    }
}
