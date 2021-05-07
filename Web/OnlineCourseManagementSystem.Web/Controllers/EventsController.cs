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

            return this.RedirectToAction("AllCreated", "Events", new { Id = user.Id });
        }

        public IActionResult All()
        {
            AllEventsViewModel viewModel = new AllEventsViewModel
            {
                EventsComing = this.eventsService.GetAllComing<EventViewModel>(),
                EventsFinished = this.eventsService.GetAllFinished<EventViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllCreated()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllEventsCreatedViewModel viewModel = new AllEventsCreatedViewModel();
            if (user.LecturerId == null)
            {
                viewModel.Events = this.eventsService.GetAll<EventViewModel>();
            }
            else
            {
                viewModel.Events = this.eventsService.GetAllCreatedByUserId<EventViewModel>(user.Id);
            }

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

            return this.RedirectToAction("AllCreated", "Events");
        }

        public async Task<IActionResult> Disapprove(int id)
        {
            await this.eventsService.Disapprove(id);

            return this.RedirectToAction("AllCreated", "Events");
        }
    }
}
