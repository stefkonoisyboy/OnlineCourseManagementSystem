namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Mails;

    public interface IMailsService
    {
        Task SendEmailAsync(MailRequest mailRequest);

        Task SendWelcomeEmailAsync(WelcomeRequest request);

        Task SendConfirmEmailForSubscriber(ConfirmMailSubscriberRequest request);
    }
}
