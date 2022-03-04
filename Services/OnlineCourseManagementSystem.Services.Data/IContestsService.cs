using OnlineCourseManagementSystem.Web.ViewModels.Contests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IContestsService
    {
        Task CreateAsync(CreateContestInputModel input);

        Task UpdateAsync(EditContestInputModel input);

        Task DeleteAsync(int id);

        int GetAllContestsCount();

        string GetContestNameById(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>(int id, int itemsPerPage = 10);

        IEnumerable<T> GetAllActive<T>();

        IEnumerable<T> GetAllFinished<T>();
    }
}
