namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System.Collections;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.ContactMessages;

    public class ContactMessagesController : Controller
    {
        private readonly IContactMessagesService contactMessagesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContactMessagesController(IContactMessagesService contactMessagesService, UserManager<ApplicationUser> userManager)
        {
            this.contactMessagesService = contactMessagesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult All()
        {
            var contactMessages = this.contactMessagesService.GetAll<ContactMessageViewModel>();
            return this.View(contactMessages);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> MarkAsSeen(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            MarkContactMessageAsSeenInputModel inputModel = new MarkContactMessageAsSeenInputModel();
            inputModel.ContactMessageId = id;
            inputModel.UserId = user.Id;
            await this.contactMessagesService.MarkAsSeen(inputModel);

            return this.RedirectToAction("All", "ContactMessages");
        }
    }
}
