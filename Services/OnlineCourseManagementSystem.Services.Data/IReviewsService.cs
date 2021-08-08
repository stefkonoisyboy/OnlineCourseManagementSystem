using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IReviewsService
    {
        IEnumerable<T> GetAllByCourseId<T>(int id);
    }
}
