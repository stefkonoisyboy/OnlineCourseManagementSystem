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
        /// <summary>
        /// Create exam.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateExamInputModel input);

        /// <summary>
        /// Update exam.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EditExamInputModel input);

        /// <summary>
        /// Delete course.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Take exam.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="userId"></param>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        Task<double> TakeExamAsync(int examId, string userId, IFormCollection formCollection);

        /// <summary>
        /// Save answer.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        Task SaveAnswerAsync(string userId, IFormCollection formCollection);

        /// <summary>
        /// Mark as seen
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task MarkAsSeenAsync(string userId, int id);

        /// <summary>
        /// Add exam to lecture.
        /// </summary>
        /// <param name="lectureId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddExamToLectureAsync(int lectureId, AddExamToLectureInputModel input);

        /// <summary>
        /// Add exam to certificate.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddExamToCertificateAsync(AddExamToCertificateInputModel input);

        /// <summary>
        /// Get name by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetNameById(int id);

        /// <summary>
        /// Get course name by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetCourseNameById(int id);

        /// <summary>
        /// Get exam id by question id.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        int GetExamIdByQuestionId(int questionId);

        /// <summary>
        /// Get duration by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int GetDurationById(int id);

        /// <summary>
        /// Get exams count by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        int GetExamsCountByUserId(string userId, string searchItem);

        /// <summary>
        /// Get exams count by user id and subject id.
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="userId"></param>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        int GetExamsCountByUserIdAndSubjectId(int subjectId, string userId, string searchItem);

        /// <summary>
        /// Get results count by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        int GetResultsCountByUserId(string userId, string searchItem);

        /// <summary>
        /// Get points by user id and exam id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        int GetPointsByUserIdAndExamId(string userId, int examId);

        /// <summary>
        /// Get course by id exam.
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        int GetCourseIdByExam(int examId);

        /// <summary>
        /// Get certificated exam id by course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        int GetCertificatedExamIdByCourse(int courseId);

        /// <summary>
        /// Get count of users with lower grades on certain exam.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        double GetCountOfUsersWithLowerGradesOnCertainExam(int examId, double grade);

        /// <summary>
        /// Get grade by user id and course id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        double GetGradeByUserIdAndCourseId(string userId, int courseId);

        /// <summary>
        /// Get exam id by user id and course id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        int GetExamIdByUserIdAndCourseId(string userId, int courseId);

        /// <summary>
        /// Get count of all users who passed certain exam.
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        int GetCountOfAllUsersWhoPassedCertainExam(int examId);

        /// <summary>
        /// Is exam added to lecture.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        bool IsExamAddedToLecture(int examId, int lectureId);

        /// <summary>
        /// Has user made certain exam.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool HasUserMadeCertainExam(int examId, string userId);

        /// <summary>
        /// Check if exam is certificated.
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        bool IsExamCertificated(int examId);

        /// <summary>
        /// Check if certificate can be certificated.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool CanStartCertificate(int courseId, string userId);

        /// <summary>
        /// Check if exam is active.
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        bool IsExamActive(int examId);

        /// <summary>
        /// Get start date by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DateTime GetStartDateById(int id);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Get by exam id and user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        T GetByExamIdAndUserId<T>(string userId, int examId);

        /// <summary>
        /// Get all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>(int page, string input, int itemsPerPage = 5);

        /// <summary>
        /// Get all by user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="searchItem"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByUserId<T>(int id, string userId, string searchItem, int itemsPerPage = 5);

        /// <summary>
        /// Get all by user id and subject id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjectId"></param>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="searchItem"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByUserIdAndSubjectId<T>(int subjectId, int id, string userId, string searchItem, int itemsPerPage = 5);

        /// <summary>
        /// Get all by current user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="searchItem"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCurrentUserId<T>(int id, string userId, string searchItem, int itemsPerPage = 5);

        /// <summary>
        /// Get all by lecture id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByLectureId<T>(int lectureId);

        /// <summary>
        /// Get all by admin.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllByAdmin<T>();

        /// <summary>
        /// Get all exams as select list items by creator id.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllExamsAsSelectListItemsByCreatorId(string creatorId);

        /// <summary>
        /// Check if exam is already used.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        bool CheckAlreadyUsedExam(int examId, int courseId);

        /// <summary>
        /// Get all exams count.
        /// </summary>
        /// <returns></returns>
        int GetAllExamsCount(string input);
    }
}
