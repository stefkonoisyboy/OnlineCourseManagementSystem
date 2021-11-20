namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Mails;
    using OnlineCourseManagementSystem.Web.ViewModels.Subscribers;

    public class SubscribersController : Controller
    {
        private readonly ISubscribersService subscribersService;
        private readonly IMailsService mailsService;

        public SubscribersController(ISubscribersService subscribersService, IMailsService mailsService)
        {
            this.subscribersService = subscribersService;
            this.mailsService = mailsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                string subscriberId = await this.subscribersService.CreateAsync(inputModel);
                ConfirmMailSubscriberRequest request = new ConfirmMailSubscriberRequest()
                {
                    ToEmail = inputModel.Email,
                    SubscriberId = subscriberId,
                };

                await this.mailsService.SendConfirmEmailForSubscriber(request);
                this.TempData["Message"] = "Please confirm subscription in the received message in your email!";
            }
            catch (Exception ex)
            {
                this.TempData["Alert"] = ex.Message;
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult ConfirmSubscription(string id)
        {
            ConfirmSubscriptionInputModel inputModel = this.subscribersService.GetById<ConfirmSubscriptionInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmSubscription(ConfirmSubscriptionInputModel inputModel)
        {
            try
            {
                await this.subscribersService.ConfirmSubscriptionAsync(inputModel);
                this.TempData["Message"] = "Confirmation Succeeded!";
            }
            catch (Exception ex)
            {
                this.TempData["Alert"] = ex.Message;
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
