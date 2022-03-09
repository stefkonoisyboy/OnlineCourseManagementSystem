namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICertificatesService
    {
        /// <summary>
        /// Get all by user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByUserId<T>(string userId);

        /// <summary>
        /// Get by user and course.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        T GetByUserIdAndCourseId<T>(string userId, int courseId);
    }
}
