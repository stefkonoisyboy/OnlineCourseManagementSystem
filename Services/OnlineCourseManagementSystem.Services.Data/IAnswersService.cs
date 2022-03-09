namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IAnswersService
    {
        /// <summary>
        /// Get all by exam and user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="examId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByExamIdAndUserId<T>(int examId, string userId);
    }
}
