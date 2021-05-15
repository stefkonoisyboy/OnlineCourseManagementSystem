namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Exams;

    public interface IExamsService
    {
        Task CreateAsync(CreateExamInputModel input);

        Task UpdateAsync(EditExamInputModel input);

        Task DeleteAsync(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();
    }
}
