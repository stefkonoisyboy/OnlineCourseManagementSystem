namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISkillsService
    {
        /// <summary>
        /// Get all by course id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCourseId<T>(int id);
    }
}
