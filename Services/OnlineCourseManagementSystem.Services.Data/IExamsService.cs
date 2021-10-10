namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;

    public interface IExamsService
    {
        Task CreateAsync(CreateExamInputModel input);

        Task UpdateAsync(EditExamInputModel input);

        Task DeleteAsync(int id);

        Task<double> TakeExamAsync(int examId, string userId, IFormCollection formCollection);

        Task SaveAnswerAsync(string userId, IFormCollection formCollection);

        Task MarkAsSeenAsync(int id);

        Task AddExamToLectureAsync(int lectureId, AddExamToLectureInputModel input);

        Task AddExamToCertificateAsync(AddExamToCertificateInputModel input);

        string GetNameById(int id);

        string GetCourseNameById(int id);

        int GetDurationById(int id);

        int GetPointsByUserIdAndExamId(string userId, int examId);

        int GetCourseIdByExam(int examId);

        int GetCertificatedExamIdByCourse(int courseId);

        double GetCountOfUsersWithLowerGradesOnCertainExam(int examId, double grade);

        double GetGradeByUserIdAndCourseId(string userId, int courseId);

        int GetExamIdByUserIdAndCourseId(string userId, int courseId);

        int GetCountOfAllUsersWhoPassedCertainExam(int examId);

        bool IsExamAddedToLecture(int examId, int lectureId);

        bool HasUserMadeCertainExam(int examId, string userId);

        bool IsExamCertificated(int examId);

        bool CanStartCertificate(int courseId, string userId);

        DateTime GetStartDateById(int id);

        T GetById<T>(int id);

        T GetByExamIdAndUserId<T>(string userId, int examId);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllByUserId<T>(string userId);

        IEnumerable<T> GetAllByCurrentUserId<T>(string userId);

        IEnumerable<T> GetAllByLectureId<T>(int lectureId);

        IEnumerable<T> GetAllByAdmin<T>();

        IEnumerable<SelectListItem> GetAllExamsAsSelectListItemsByCreatorId(string creatorId);
    }
}
