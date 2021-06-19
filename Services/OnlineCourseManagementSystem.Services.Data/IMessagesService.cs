namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task Create(CreateMessageInputModel inputModel);

        IEnumerable<T> GetAllBy<T>(int chatId);
    }
}
