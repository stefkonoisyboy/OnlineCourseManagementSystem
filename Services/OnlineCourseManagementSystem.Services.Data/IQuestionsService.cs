namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Questions;

    public interface IQuestionsService
    {
        /// <summary>
        /// Create question.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateQuestionInputModel input);

        /// <summary>
        /// Update question.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EditQuestionInputModel input);

        /// <summary>
        /// Delete question.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get count by exam id.
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        int GetCountByExamId(int examId, string input);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Get all by exam.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="examId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByExam<T>(int examId, int page, string input, int itemsPerPage = 5);
    }
}
