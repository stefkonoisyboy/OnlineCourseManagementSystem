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

        /// <summary>
        /// This method gets all chats for user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllBy<T>(string userId);

        /// <summary>
        /// This method gets all users that are not added to given chat.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> GetAllUsersNotAddedBy(int chatId);

        /// <summary>
        /// This method removes user from chat.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        Task RemoveUserFromChat(string userId, int chatId);

        /// <summary>
        /// This method gets creatorId for chat.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        string GetCreatorIdBy(int chatId);

        /// <summary>
        /// Thsi mehtod gets chat.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="chatId"></param>
        /// <returns></returns>
        T GetBy<T>(int chatId);

        /// <summary>
        /// Thsi method gets name's(forming it) chat.
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetNameBy(int? chatId, string userId);

        /// <summary>
        /// This method gets all users for chat.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="chatId"></param>
        /// <returns></returns>
        IEnumerable<T> GetUsersByChat<T>(int chatId);

        Task LeaveChat(int chatId, string userId);

        Task PinChat(int chatId, string userId);

        Task UnPinChat(int chatId, string userId);

        Task MuteChat(int chatId, string userId);

        Task UnmuteChat(int chatId, string userId);

        /// <summary>
        /// This mtethod gets all chats that are pinned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllPinnedBy<T>(string userId);

        /// <summary>
        /// This method gets chat for current user and chat.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        T GetByCurrentUser<T>(string userId, int chatId);

        /// <summary>
        /// This method gets profileImage of second user as chat's icon url
        /// for normal chat.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        string GetIconUrl(string userId, int chatId);
    }
}
