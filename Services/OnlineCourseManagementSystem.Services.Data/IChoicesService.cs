using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IChoicesService
    {
        IEnumerable<T> GetAllById<T>(int id);
    }
}
