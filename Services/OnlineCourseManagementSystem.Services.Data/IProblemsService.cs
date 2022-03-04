using OnlineCourseManagementSystem.Web.ViewModels.Problems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IProblemsService
    {
        Task CreateAsync(CreateProblemInputModel input);

        Task UpdateAsync(EditProblemInputModel input);

        Task DeleteAsync(int id);

        int GetAllProblemsCountByContestId(int contestId);

        int GetContestIdByProblemId(int problemId);

        T GetById<T>(int id);

        IEnumerable<T> GetAllByContestIdViewModel<T>(int contestId, int problemId, int id, int itemsPerPage = 10);
    }
}
