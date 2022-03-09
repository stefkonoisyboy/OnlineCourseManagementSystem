namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICompletitionsService
    {
        /// <summary>
        /// Get all completitions count by course id and user id.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetAllCompletitionsCountByCourseIdAndUserId(int courseId, string userId);
    }
}
