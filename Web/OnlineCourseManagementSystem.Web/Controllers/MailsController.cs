namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Mails;

    public class MailsController : Controller
    {
        private readonly IMailsService mailsService;

        public MailsController(IMailsService mailsService)
        {
            this.mailsService = mailsService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult SendMail()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> SendMail(MailRequest request)
        {
            try
            {
                await this.mailsService.SendEmailAsync(request);
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult SendWelcomeMail()
        {
            string url = this.Request.Path;
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> SendWelcomeMail(WelcomeRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }


            await this.mailsService.SendWelcomeEmailAsync(request);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
