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

        Task AddWordFileAsync(AddWordDocumentToLectureInputModel input);

        Task AddPresentationFileAsync(AddPresentationToLectureInputModel input);

        Task AddVideoAsync(AddVideoToLectureInputModel input);

        int GetLecturesCountByCreatorId(string creatorId);

        int GetCourseIdByLectureId(int id);

        int GetLecturesCountById(int courseId);

        string GetNameById(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAllById<T>(int courseId, int page, int itemsPerPage = 3);

        IEnumerable<T> GetAllByFileId<T>(int id);

        IEnumerable<T> GetAllByCreatorId<T>(int page, string creatorId, int items = 3);

        IEnumerable<T> GetAllVideosById<T>(int lectureId);

        IEnumerable<T> GetAllInInterval<T>(DateTime startDate, DateTime endDate);

        IEnumerable<T> GetAllByAdmin<T>();
    }
}
