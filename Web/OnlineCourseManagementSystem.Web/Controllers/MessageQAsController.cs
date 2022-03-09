namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.Hubs;
    using OnlineCourseManagementSystem.Web.ViewModels.MessageQAs;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class MessageQAsController : Controller
    {
        private readonly IMessageQAsService messageQAsService;
        private readonly IChannelsService channelsService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IHubContext<QAHub> context;

        public MessageQAsController(
            IMessageQAsService messageQAsService,
            IChannelsService channelsService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IHubContext<QAHub> context)
        {
            this.messageQAsService = messageQAsService;
            this.channelsService = channelsService;
            this.usersService = usersService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [Authorize(Roles = "Student,Lecturer")]
        [HttpGet]
        public async Task<IActionResult> AllByChannel(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);
            this.ViewBag.RoleName = roles.FirstOrDefault();
            this.ViewBag.Id = id;

            string channelName = this.channelsService.GetChannelNameById(id);

            if (this.User.IsInRole(GlobalConstants.StudentRoleName))
            {
                BreadcrumbNode mychannelsNode = new MvcBreadcrumbNode("AllByParticipant", "Channels", "My Channels");

                BreadcrumbNode channelNode = new MvcBreadcrumbNode("AllByChannel", "MessageQAs", channelName)
                {
                    Parent = mychannelsNode,
                };

                this.ViewData["BreadcrumbNode"] = channelNode;
            }
            else if (this.User.IsInRole(GlobalConstants.LecturerRoleName))
            {
                BreadcrumbNode mychannelsNode = new MvcBreadcrumbNode("AllByCreator", "Channels", "My Channels");

                BreadcrumbNode channelNode = new MvcBreadcrumbNode("AllByChannel", "MessageQAs", channelName)
                {
                    Parent = mychannelsNode,
                };

                this.ViewData["BreadcrumbNode"] = channelNode;
            }

            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpGet]
        public async Task<IActionResult> Archive(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);
            this.ViewBag.RoleName = roles.FirstOrDefault();
            this.ViewBag.Id = id;
            return this.View();
        }

        [Authorize(Roles = "Student,Lecturer")]
        [HttpGet]
        public async Task<IActionResult> Recent(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);
            this.ViewBag.RoleName = roles.FirstOrDefault();
            this.ViewBag.Id = id;
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateMessageQAInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                AllMessagesByChannleIdListViewModel viewModel = new AllMessagesByChannleIdListViewModel
                {
                    Messages = this.messageQAsService.GetAllMessagesByChannelId<AllMessagesByChannelIdViewModel>(id),
                    CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                    ChannelId = id,
                };

                return this.View("AllByChannel", viewModel);
            }

            input.CreatorId = user.Id;
            input.ChannelId = id;
            await this.messageQAsService.CreateAsync(input);
            this.TempData["Message"] = "You have successfully added message to this channel!";
            await this.context.Clients.All.SendAsync("refreshMessages", id);

            return this.RedirectToAction(nameof(this.AllByChannel), new { id });
        }
    }
}
