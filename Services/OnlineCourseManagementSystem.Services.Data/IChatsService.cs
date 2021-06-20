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

        IEnumerable<T> GetUserByChat<T>(int chatId);
    }
}
