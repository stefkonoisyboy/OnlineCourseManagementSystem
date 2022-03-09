namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Tests;

    public interface ITestsService
    {
        /// <summary>
        /// Create test.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateTestInputModel input);

        /// <summary>
        /// Update test.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EditTestInputModel input);

        /// <summary>
        /// Delete test.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Get all by problem id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="problemId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByProblemId<T>(int problemId);
    }
}
