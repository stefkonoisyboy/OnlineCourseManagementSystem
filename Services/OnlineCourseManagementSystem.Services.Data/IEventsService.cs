namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Events;

    public interface IEventsService
    {
        Task CreateAsync(CreateEventInputModel inputModel);

        IEnumerable<T> GetAllCreatedByUserId<T>(string userId);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int eventId);

        Task Approve(int eventId);

        Task Disapprove(int eventId);

        IEnumerable<T> GetAllComing<T>();

        IEnumerable<T> GetAllFinished<T>();
    }
}
