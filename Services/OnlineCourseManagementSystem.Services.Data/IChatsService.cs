namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Chats;

    public interface IChatsService
    {
        Task CreateAsync(CreateChatInputModel inputModel);

        IEnumerable<T> GetAllBy<T>(string userId);

        string GetNameBy(int? chatId);

        IEnumerable<T> GetUsersByChat<T>(int chatId);

        Task LeaveChat(int chatId, string userId);

        Task PinChat(int chatId, string userId);

        Task UnPinChat(int chatId, string userId);

        Task MuteChat(int chatId, string userId);

        Task UnmuteChat(int chatId, string userId);

        IEnumerable<T> GetAllPinnedBy<T>(string userId);
    }
}
