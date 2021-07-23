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

        IEnumerable<T> GetAllBy<T>(int chatId);

        T GetMessageBy<T>(int messageId);

        Task MarkAsSeeen(int messageId);

        int AllUnseenMessagesCountBy(int chatId, int userId);

        IEnumerable<T> SearchMessages<T>(SearchInputModel inputModel);
    }
}
