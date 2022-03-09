namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Channels;
    using SmartBreadcrumbs.Attributes;

    public class ChannelsController : Controller
    {
        private readonly IChannelsService channelsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChannelsController(IChannelsService channelsService, UserManager<ApplicationUser> userManager)
        {
            this.channelsService = channelsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("My Channels", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult AllByCreator(string id)
        {
            UserIdViewModel viewModel = new UserIdViewModel
            {
                UserId = id,
            };
            this.ViewData["CurrentUserHeading"] = "Messages";
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb("My Channels", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult AllByParticipant()
        {
            this.ViewData["CurrentUserHeading"] = "Messages";
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateChannelInputModel input)
        {
            try
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);
                input.CreatorId = user.Id;
                await this.channelsService.CreateAsync(input);
                this.TempData["Message"] = "Channel has been successfully scheduled!";
            }
            catch (Exception ex)
            {
                this.TempData["Alert"] = ex.Message;
            }

            return this.RedirectToAction(nameof(this.AllByCreator));
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [HttpPost]
        public async Task<IActionResult> JoinChannel(JoinChannelInputModel input, IFormCollection formCollection)
        {
            try
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);
                input.UserId = user.Id;
                input.Code = formCollection["Code"].ToString();
                await this.channelsService.JoinChannelAsync(input);
                this.TempData["Message"] = "You have successfully joined a channel!";
            }
            catch (Exception ex)
            {
                this.TempData["Alert"] = ex.Message;
            }

            return this.RedirectToAction(nameof(this.AllByParticipant));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.channelsService.DeleteAsync(id);
            this.TempData["Message"] = "Channel has been deleted successfully!";
            return this.RedirectToAction(nameof(this.AllByCreator));
        }
    }
}
