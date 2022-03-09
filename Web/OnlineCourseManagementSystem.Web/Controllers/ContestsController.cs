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
    using OnlineCourseManagementSystem.Web.ViewModels.Contests;
    using OnlineCourseManagementSystem.Web.ViewModels.Submissions;
    using SmartBreadcrumbs.Attributes;

    public class ContestsController : Controller
    {
        private readonly IContestsService contestsService;
        private readonly ISubmissionsService submissionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContestsController(IContestsService contestsService, ISubmissionsService submissionsService, UserManager<ApplicationUser> userManager)
        {
            this.contestsService = contestsService;
            this.submissionsService = submissionsService;
            this.userManager = userManager;
        }

        [Authorize]
        [Breadcrumb(" Index ", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult Index()
        {
            AllContestsIndexListViewModel viewModel = new AllContestsIndexListViewModel
            {
                ActiveContests = this.contestsService.GetAllActive<AllActiveContestsViewModel>(),
                FinishedContests = this.contestsService.GetAllFinished<AllFinishedContestsViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [Breadcrumb(" All ", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 10;

            AllContestsListViewModel viewModel = new AllContestsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ActiveCoursesCount = this.contestsService.GetAllContestsCount(),
                Contests = this.contestsService.GetAll<AllContestsViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [Breadcrumb(" Contest Details ", FromAction = "All")]
        public async Task<IActionResult> Details(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            ContestsDetailsViewModel viewModel = this.contestsService.GetById<ContestsDetailsViewModel>(id);
            viewModel.Submissions = this.submissionsService.GetTop5ByContestIdAndUserId<Top5SubmissionsByUserIdAndContestIdViewModel>(id, user.Id);

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb(" Create ", FromAction = "All")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateContestInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.contestsService.CreateAsync(input);
            this.TempData["Message"] = "Contest created successfully!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb(" Edit ", FromAction = "All")]
        public IActionResult Edit(int id)
        {
            EditContestInputModel input = this.contestsService.GetById<EditContestInputModel>(id);
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditContestInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.contestsService.UpdateAsync(input);
            this.TempData["Message"] = "Contest updated successfully!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.contestsService.DeleteAsync(id);
            this.TempData["Message"] = "Contest deleted successfully!";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
