namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;

    public interface ILecturesService
    {
        Task CreateAsync(CreateLectureInputModel input);

        Task UpdateAsync(EditLectureInputModel input);

        Task DeleteAsync(int id);

        Task AddResourceAsync(AddFileToLectureInputModel input);

        IEnumerable<T> GetAllById<T>(int id);
    }
}
