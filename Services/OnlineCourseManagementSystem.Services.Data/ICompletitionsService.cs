using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface ICompletitionsService
    {
        int GetAllCompletitionsCountByCourseIdAndUserId(int courseId, string userId);
    }
}
