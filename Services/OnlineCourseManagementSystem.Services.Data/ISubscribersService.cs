namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Subscribers;

    public interface ISubscribersService
    {
        /// <summary>
        /// This method creates subscriber.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task<string> CreateAsync(CreateSubscriberInputModel inputModel);

        /// <summary>
        /// This method confirms subscribtion.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task ConfirmSubscriptionAsync(ConfirmSubscriptionInputModel inputModel);

        /// <summary>
        /// This method gets subscriber.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(string id);

        bool? CheckSubscribedByEmail(string email);
    }
}
