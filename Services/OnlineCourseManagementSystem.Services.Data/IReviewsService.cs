namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;

    public interface IReviewsService
    {
        /// <summary>
        /// Create review.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateReviewInputModel input);

        /// <summary>
        /// Get all by course id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCourseId<T>(int id);

        /// <summary>
        /// Get top 3 recent.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetTop3Recent<T>();
    }
}
