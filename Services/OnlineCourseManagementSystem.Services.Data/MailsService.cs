namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using MailKit;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using OnlineCourseManagementSystem.Web.ViewModels.Mails;
    using OnlineCourseManagementSystem.Web.ViewModels.Mails.Settings;

    public class MailsService : IMailsService
    {
        private readonly MailSettings mailSettings;

        public MailsService(IOptions<MailSettings> mailsettings)
        {
            this.mailSettings = mailsettings.Value;
        }

        public async Task SendConfirmEmailForSubscriber(ConfirmMailSubscriberRequest request)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\wwwroot\\Templates\\ConfirmSubscriberMail.html";
            StreamReader str = new StreamReader(filePath);
            string mailText = str.ReadToEnd();
            str.Close();
            mailText = mailText.Replace("[email]", request.ToEmail).Replace("[subsriber-id]", request.SubscriberId);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(this.mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Confirm your subscription {request.ToEmail}";
            var builder = new BodyBuilder();
            builder.HtmlBody = mailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(this.mailSettings.Host, this.mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(this.mailSettings.Mail, this.mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(this.mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(this.mailSettings.Host, this.mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(this.mailSettings.Mail, this.mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\wwwroot\\Templates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(filePath);
            string mailText = str.ReadToEnd();
            str.Close();
            mailText = mailText.Replace("[email]", request.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(this.mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.ToEmail}";
            var builder = new BodyBuilder();
            builder.HtmlBody = mailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(this.mailSettings.Host, this.mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(this.mailSettings.Mail, this.mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
