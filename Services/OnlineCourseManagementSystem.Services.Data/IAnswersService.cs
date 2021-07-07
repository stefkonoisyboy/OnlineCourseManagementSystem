using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IAnswersService
    {
        IEnumerable<T> GetAllByExamIdAndUserId<T>(int examId, string userId);
    }
}
