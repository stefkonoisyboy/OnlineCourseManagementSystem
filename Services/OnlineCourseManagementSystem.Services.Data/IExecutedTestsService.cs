using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IExecutedTestsService
    {
        /// <summary>
        /// Get all by submission id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllBySubmission<T>(int submissionId);
    }
}
