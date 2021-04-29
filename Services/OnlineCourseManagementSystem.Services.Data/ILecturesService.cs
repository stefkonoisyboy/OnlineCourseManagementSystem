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

        Task AddWordFileAsync(AddFileToLectureInputModel input);

        Task AddPresentationFileAsync(AddFileToLectureInputModel input);

        Task AddVideoAsync(AddVideoToLectureInputModel input);

        IEnumerable<T> GetAllById<T>(int courseId);

        IEnumerable<T> GetAllVideosById<T>(int lectureId);

        IEnumerable<T> GetAllInInterval<T>(DateTime startDate, DateTime endDate);
    }
}
