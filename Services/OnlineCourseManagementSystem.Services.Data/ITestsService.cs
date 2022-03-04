using OnlineCourseManagementSystem.Web.ViewModels.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface ITestsService
    {
        Task CreateAsync(CreateTestInputModel input);

        Task UpdateAsync(EditTestInputModel input);

        Task DeleteAsync(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAllByProblemId<T>(int problemId);
    }
}
