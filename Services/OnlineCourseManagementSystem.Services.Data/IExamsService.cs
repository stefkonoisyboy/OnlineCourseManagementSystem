namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;

    public interface IExamsService
    {
        Task CreateAsync(CreateExamInputModel input);

        Task UpdateAsync(EditExamInputModel input);

        Task DeleteAsync(int id);

        Task<double> TakeExamAsync(int examId, string userId, IFormCollection formCollection);

        Task SaveAnswerAsync(string userId, IFormCollection formCollection);

        Task MarkAsSeenAsync(int id);

        string GetNameById(int id);

        string GetCourseNameById(int id);

        int GetDurationById(int id);

        int GetPointsByUserIdAndExamId(string userId, int examId);

        double GetCountOfUsersWithLowerGradesOnCertainExam(int examId, double grade);

        int GetCountOfAllUsersWhoPassedCertainExam(int examId);

        DateTime GetStartDateById(int id);

        T GetById<T>(int id);

        T GetByExamIdAndUserId<T>(string userId, int examId);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllByUserId<T>(string userId);

        IEnumerable<T> GetAllByCurrentUserId<T>(string userId);

        IEnumerable<T> GetAllByAdmin<T>();
    }
}
