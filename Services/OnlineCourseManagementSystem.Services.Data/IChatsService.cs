namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Chats;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public interface IChatsService
    {
        Task CreateAsync(CreateChatInputModel inputModel);

        Task UpdateAsync(EditChatInputModel inputModel);

        Task AddUsersToChat(AddUsersToChatInputModel inputModel);

        IEnumerable<T> GetAllBy<T>(string userId);

        IEnumerable<UserViewModel> GetAllUsersNotAddedBy(int chatId);

        Task RemoveUserFromChat(string userId, int chatId);

        string GetCreatorIdBy(int chatId);

        T GetBy<T>(int chatId);

        string GetNameBy(int? chatId, string userId);

        IEnumerable<T> GetUsersByChat<T>(int chatId);

        Task LeaveChat(int chatId, string userId);

        Task PinChat(int chatId, string userId);

        Task UnPinChat(int chatId, string userId);

        Task MuteChat(int chatId, string userId);

        Task UnmuteChat(int chatId, string userId);

        IEnumerable<T> GetAllPinnedBy<T>(string userId);

        T GetByCurrentUser<T>(string userId, int chatId);

        string GetIconUrl(string userId, int chatId);
    }
}
