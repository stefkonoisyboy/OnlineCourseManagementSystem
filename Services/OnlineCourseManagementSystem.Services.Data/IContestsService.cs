namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Contests;

    public interface IContestsService
    {

        /// <summary>
        /// Create contest.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateContestInputModel input);

        /// <summary>
        /// Update contest.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EditContestInputModel input);

        /// <summary>
        /// Delete contest.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get all contests count.
        /// </summary>
        /// <returns></returns>
        int GetAllContestsCount();

        /// <summary>
        /// Get contest name by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetContestNameById(int id);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Get all contests.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>(int id, int itemsPerPage = 10);

        /// <summary>
        /// Get all active contests.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllActive<T>();

        /// <summary>
        /// Get all finished contests.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllFinished<T>();
    }
}
