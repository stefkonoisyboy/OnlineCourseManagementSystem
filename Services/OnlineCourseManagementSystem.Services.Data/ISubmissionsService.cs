namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Submissions;

    public interface ISubmissionsService
    {
        /// <summary>
        /// Create submission.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputPath"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        Task CreateAsync(CreateSubmissionInputModel input, string inputPath, string outputPath);

        /// <summary>
        /// Get top 5 by contest id and user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contestId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetTop5ByContestIdAndUserId<T>(int contestId, string userId);

        /// <summary>
        /// Get top 5 problem id and user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="problemId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetTop5ByProblemIdAndUserId<T>(int problemId, string userId);

        /// <summary>
        /// Get problem id by submission id.
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        int GetProblemIdBySubmissionId(int submissionId);
    }
}
