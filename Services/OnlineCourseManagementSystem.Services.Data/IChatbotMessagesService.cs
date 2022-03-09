namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages;

    public interface IChatbotMessagesService
    {
        /// <summary>
        /// Create message.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateChatbotMessageInputModel input);

        /// <summary>
        /// Get all by creator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCreatorId<T>(string creatorId);
    }
}
