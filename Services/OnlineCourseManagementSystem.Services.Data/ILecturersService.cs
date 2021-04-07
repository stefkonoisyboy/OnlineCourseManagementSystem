namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILecturersService
    {
        IEnumerable<T> GetAll<T>();
    }
}
