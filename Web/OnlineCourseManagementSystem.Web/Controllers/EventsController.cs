namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Events;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;

        public EventsController(IEventsService eventsService, UserManager<ApplicationUser> userManager)
        {
            this.eventsService = eventsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Lecturer,Administrator")]
        public IActionResult Create()
        {
            BreadcrumbNode mycreatedEventsNode = new MvcBreadcrumbNode("AllCreated", "Events", "My Created Events");
            BreadcrumbNode createNode = new MvcBreadcrumbNode("Create", "Events", "Create Event")
            {
                Parent = mycreatedEventsNode,
            };

            this.ViewData["BreadcrumbNode"] = createNode;
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer,Administrator")]
        public async Task<IActionResult> Create(CreateEventInputModel inputModel)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            inputModel.CreatorId = user.Id;
            await this.eventsService.CreateAsync(inputModel);
            this.TempData["Message"] = "Successfully created event!";

            return this.RedirectToAction("AllCreated", "Events", new { Id = user.Id });
        }

        [Breadcrumb("All Events", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult All(int pageNumber = 1)
        {
            AllEventsViewModel viewModel = new AllEventsViewModel
            {
                EventsComing = this.eventsService.GetAllComing<EventViewModel>(),
                EventsFinished = this.eventsService.GetAllFinished<EventViewModel>(),
            };

            pageNumber = Math.Max(1, pageNumber);
            int skip = (pageNumber - 1) * viewModel.ItemsPerPage;
            List<EventViewModel> query = viewModel
                    .EventsFinished
                    .Skip(skip)
                    .Take(viewModel.ItemsPerPage)
                    .ToList();

            viewModel.PageNumber = pageNumber;
            int eventsCount = viewModel.EventsFinished.ToList().Count();
            viewModel.PagesCount = (int)Math.Ceiling((double)eventsCount / viewModel.ItemsPerPage);
            viewModel.EventsFinished = query.ToList();

            return this.View(viewModel);
        }

        [Authorize(Roles = "Lecturer,Administrator")]
        [Breadcrumb("My Created Evens", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> AllCreated()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllEventsCreatedViewModel viewModel = new AllEventsCreatedViewModel();

            viewModel.Events = this.eventsService.GetAllCreatedByUserId<EventViewModel>(user.Id);
            viewModel.InputModel = new VideoFileInputModel();
            return this.View(viewModel);
        }

        public IActionResult EventInfo(int id)
        {
            var @event = this.eventsService.GetById<EventInfoViewModel>(id);

            BreadcrumbNode breadcrumbNode;
            if ((string)this.ViewData["Title"] == "My Created Events")
            {
                breadcrumbNode = new MvcBreadcrumbNode("AllCreated", "Events", "My Created Events");
            }
            else
            {
                breadcrumbNode = new MvcBreadcrumbNode("All", "Events", "Events");
            }

            BreadcrumbNode eventInfoNode = new MvcBreadcrumbNode("EventInfo", "Events", "Event Info")
            {
                Parent = breadcrumbNode,
            };

            this.ViewData["BreadcrumbNode"] = eventInfoNode;

            return this.View(@event);
        }

        [Authorize(Roles = "Lecturer,Administrator")]
        public IActionResult Edit(int id)
        {
            BreadcrumbNode mycreatedEventsNode = new MvcBreadcrumbNode("AllCreated", "Events", "My Created Events");
            BreadcrumbNode editNode = new MvcBreadcrumbNode("Edit", "Events", "Edit Event")
            {
                Parent = mycreatedEventsNode,
            };

            this.ViewData["BreadcrumbNode"] = editNode;
            EditEventInputModel inputModel = this.eventsService.GetById<EditEventInputModel>(id);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles= "Lecturer,Administrator")]
        public async Task<IActionResult> Edit(EditEventInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel = this.eventsService.GetById<EditEventInputModel>(inputModel.Id);
                return this.View(inputModel);
            }

            await this.eventsService.UpdateAsync(inputModel);
            this.TempData["Message"] = "Successfully updated event!";
            return this.RedirectToAction("AllCreated", "Events");
        }

        [Authorize(Roles = "Lecturer,Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.eventsService.DeleteAsync(id);
            this.TempData["Message"] = "Successfully deleted event!";
            return this.RedirectToAction("AllCreated", "Events");
        }
    }
}
