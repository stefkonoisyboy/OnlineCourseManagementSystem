namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Shedules;

    public interface IShedulesServices
    {
        Task CreateAsync(CreateSheduleInputModel inputModel);

        IEnumerable<T> GetAllByEventId<T>(int eventId);
    }
}
