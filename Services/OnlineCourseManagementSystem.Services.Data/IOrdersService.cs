namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        /// <summary>
        /// Create order.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateAsync(int courseId, string userId);

        /// <summary>
        /// Delete order.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteAsync(int courseId, string userId);

        /// <summary>
        /// Get all by user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByUserId<T>(string userId);

        /// <summary>
        /// Get all courses in cart count.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int CoursesInCartCount(string userId);

        /// <summary>
        /// Check if order is available.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsOrderAvailable(int courseId, string userId);
    }
}
