namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task<int> Create(CreateMessageInputModel inputModel);

        Task UpdateAsync(EditMessageInputModel inputModel);

        Task DeleteAsync(int messageId);

        /// <summary>
        /// This method gets all messages for chat.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="chatId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllBy<T>(int chatId);

        /// <summary>
        /// This method gets message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageId"></param>
        /// <returns></returns>
        T GetMessageBy<T>(int messageId);

        Task MarkAsSeeen(int messageId);

        int AllUnseenMessagesCountBy(int chatId, int userId);

        /// <summary>
        /// This method searches for messages with given keywords.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        IEnumerable<T> SearchMessages<T>(SearchInputModel inputModel);
    }
}
