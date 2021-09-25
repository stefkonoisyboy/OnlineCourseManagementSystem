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

        Task UpdateModifiedOnById(int id);

        Task DeleteAsync(int id);

        Task AddWordFileAsync(AddFileToLectureInputModel input);

        Task AddPresentationFileAsync(AddFileToLectureInputModel input);

        Task AddVideoAsync(AddVideoToLectureInputModel input);

        int GetLecturesCountByCreatorId(string creatorId);

        int GetCourseIdByLectureId(int id);

        string GetNameById(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAllById<T>(int courseId);

        IEnumerable<T> GetAllByFileId<T>(int id);

        IEnumerable<T> GetAllByCreatorId<T>(int page, string creatorId, int items = 3);

        IEnumerable<T> GetAllVideosById<T>(int lectureId);

        IEnumerable<T> GetAllInInterval<T>(DateTime startDate, DateTime endDate);

        IEnumerable<T> GetAllByAdmin<T>();
    }
}
