using OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IChatbotMessagesService
    {
        Task CreateAsync(CreateChatbotMessageInputModel input);

        IEnumerable<T> GetAllByCreatorId<T>(string creatorId);
    }
}
