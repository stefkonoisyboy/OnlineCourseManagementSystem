namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Problems;

    public interface IProblemsService
    {
        /// <summary>
        /// Create problem.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateProblemInputModel input);

        /// <summary>
        /// Update problem.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EditProblemInputModel input);

        /// <summary>
        /// Delete problem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get all problems count by contest id.
        /// </summary>
        /// <param name="contestId"></param>
        /// <returns></returns>
        int GetAllProblemsCountByContestId(int contestId);

        /// <summary>
        /// Get contest id by problem id.
        /// </summary>
        /// <param name="problemId"></param>
        /// <returns></returns>
        int GetContestIdByProblemId(int problemId);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Get all by contest id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contestId"></param>
        /// <param name="problemId"></param>
        /// <param name="id"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByContestIdViewModel<T>(int contestId, int problemId, int id, int itemsPerPage = 10);
    }
}
