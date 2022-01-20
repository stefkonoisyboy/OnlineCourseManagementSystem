namespace OnlineCourseManagementSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.ContactMessages;

    public interface IContactMessagesService
    {
        Task CreateAsync(CreateContactMessageInputModel inputModel);

        /// <summary>
        /// This method gets all contact messages.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        Task MarkAsSeen(MarkContactMessageAsSeenInputModel inputModel);
    }
}
