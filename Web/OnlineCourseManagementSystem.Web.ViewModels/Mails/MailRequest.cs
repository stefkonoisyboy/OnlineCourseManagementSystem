namespace OnlineCourseManagementSystem.Web.ViewModels.Mails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class MailRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ToEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public ICollection<IFormFile> Attachments { get; set; }
    }
}
