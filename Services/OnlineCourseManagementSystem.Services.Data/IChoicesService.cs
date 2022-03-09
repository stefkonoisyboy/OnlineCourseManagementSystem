namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IChoicesService
    {
        /// <summary>
        /// Get all by by question.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllById<T>(int id);
    }
}
