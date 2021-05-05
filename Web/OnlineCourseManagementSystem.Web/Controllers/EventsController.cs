using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseManagementSystem.Common;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Data;
using OnlineCourseManagementSystem.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;

        public EventsController(IEventsService eventsService, UserManager<ApplicationUser> userManager)
        {
            this.eventsService = eventsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Create(CreateEventInputModel inputModel)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            inputModel.CreatorId = user.Id;
            await this.eventsService.CreateAsync(inputModel);

            return this.RedirectToAction("All", "Events");
        }

        public IActionResult All()
        {
            AllEventsViewModel viewModel = new AllEventsViewModel
            {
                Events = this.eventsService.GetAll<EventViewModel>(),
            };

            return this.View(viewModel);
        }


        public IActionResult EventInfo(int id)
        {
            var @event = this.eventsService.GetById<EventInfoViewModel>(id);
            return this.View(@event);
        }

        public async Task<IActionResult> Approve(int id)
        {
            await this.eventsService.Approve(id);

            this.TempData["EventApproved"] = "Successfully approved event";

            return this.RedirectToAction("All", "Events");
        }

        public async Task<IActionResult> Disapprove(int id)
        {
            await this.eventsService.Disapprove(id);
            this.TempData["Evemt Disapproved"] = "Succesfully disaproved event";

            return this.RedirectToAction("All", "Events");
        }
    }
}
