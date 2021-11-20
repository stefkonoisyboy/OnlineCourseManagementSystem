namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Subscribers;

    public interface ISubscribersService
    {
        Task<string> CreateAsync(CreateSubscriberInputModel inputModel);

        Task ConfirmSubscriptionAsync(ConfirmSubscriptionInputModel inputModel);

        T GetById<T>(string id);
    }
}
