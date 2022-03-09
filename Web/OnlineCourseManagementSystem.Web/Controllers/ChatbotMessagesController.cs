namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages;

    public class ChatbotMessagesController : Controller
    {
        private readonly IChatbotMessagesService chatbotMessagesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatbotMessagesController(IChatbotMessagesService chatbotMessagesService, UserManager<ApplicationUser> userManager)
        {
            this.chatbotMessagesService = chatbotMessagesService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Create(CreateChatbotMessageInputModel input)
        {
            try
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);
                input.CreatorId = user.Id;
                await this.chatbotMessagesService.CreateAsync(input);
                this.TempData["Message"] = "You have successfully asked our chatbot a question!";
            }
            catch (Exception ex)
            {
                this.TempData["Alert"] = ex.Message;
            }

            return this.Redirect("/");
        }
    }
}
