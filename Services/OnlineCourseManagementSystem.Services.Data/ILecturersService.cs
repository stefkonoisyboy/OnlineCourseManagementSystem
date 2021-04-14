namespace OnlineCourseManagementSystem.Services.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILecturersService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllById<T>(int courseId);

        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}
