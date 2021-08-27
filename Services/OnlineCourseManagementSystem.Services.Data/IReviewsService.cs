using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IReviewsService
    {
        Task CreateAsync(CreateReviewInputModel input);

        IEnumerable<T> GetAllByCourseId<T>(int id);

        IEnumerable<T> GetTop3Recent<T>();
    }
}
