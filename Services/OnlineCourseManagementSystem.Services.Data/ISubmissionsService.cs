using OnlineCourseManagementSystem.Web.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface ISubmissionsService
    {
        Task CreateAsync(CreateSubmissionInputModel input, string inputPath, string outputPath);

        IEnumerable<T> GetTop5ByContestIdAndUserId<T>(int contestId, string userId);

        IEnumerable<T> GetTop5ByProblemIdAndUserId<T>(int problemId, string userId);
    }
}
