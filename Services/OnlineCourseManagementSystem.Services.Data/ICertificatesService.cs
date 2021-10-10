using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface ICertificatesService
    {
        IEnumerable<T> GetAllByUserId<T>(string userId);

        T GetByUserIdAndCourseId<T>(string userId, int courseId);
    }
}
