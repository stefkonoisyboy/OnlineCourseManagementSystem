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
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITownsService townsService;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            ITownsService townsService)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.townsService = townsService;
        }

        public async Task<IActionResult> ManageAccountById()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            ManageAccountInputModel inputModel = this.usersService.GetById<ManageAccountInputModel>(user.Id);
            inputModel.TownItems = this.townsService.GetAllAsSelectListItems();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageAccountById(ManageAccountInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);

                inputModel = this.usersService.GetById<ManageAccountInputModel>(user.Id);
                inputModel.TownItems = this.townsService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            await this.usersService.UpdateAsync(inputModel);
            this.TempData["UpdatedAccount"] = "Successfully updated account";
            return this.RedirectToAction("ManageAccountById", "Users");
        }
    }
}
