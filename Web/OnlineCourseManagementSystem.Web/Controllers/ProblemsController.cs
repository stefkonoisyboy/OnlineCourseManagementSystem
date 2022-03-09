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
    using OnlineCourseManagementSystem.Web.ViewModels.Problems;
    using OnlineCourseManagementSystem.Web.ViewModels.Submissions;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly IContestsService contestsService;
        private readonly ISubmissionsService submissionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProblemsController(
            IProblemsService problemsService,
            IContestsService contestsService,
            ISubmissionsService submissionsService,
            UserManager<ApplicationUser> userManager)
        {
            this.problemsService = problemsService;
            this.contestsService = contestsService;
            this.submissionsService = submissionsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb(" Create ", FromAction = "All", FromController = typeof(ContestsController))]
        public IActionResult Create(int contestId)
        {
            CreateProblemInputModel input = new CreateProblemInputModel
            {
                ContestId = contestId,
            };
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(int contestId, CreateProblemInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.ContestId = contestId;
                return this.View(input);
            }

            input.ContestId = contestId;
            await this.problemsService.CreateAsync(input);
            this.TempData["Message"] = "Problem added successfully to the contest!";

            return this.RedirectToAction("All", "Contests");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Edit(int id)
        {
            int contestId = this.problemsService.GetContestIdByProblemId(id);
            BreadcrumbNode parentNode = new MvcBreadcrumbNode("AllByContestId", "Problems", "Problems")
            {
                RouteValues = new { problemId = id, contestId = contestId, },
            };

            BreadcrumbNode childNode = new MvcBreadcrumbNode("Edit", "Problems", "Edit")
            {
                Parent = parentNode,
                RouteValues = new { id = id },
            };

            this.ViewData["BreadcrumbNode"] = childNode;

            EditProblemInputModel input = this.problemsService.GetById<EditProblemInputModel>(id);
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProblemInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.problemsService.UpdateAsync(input);
            this.TempData["Message"] = "Problem updated successfully!";

            return this.RedirectToAction("All", "Contests");
        }

        [Authorize]
        [Breadcrumb(" Problems ", FromAction = "All", FromController = typeof(ContestsController))]
        public async Task<IActionResult> AllByContestId(int problemId, int contestId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            AllProblemsByContestIdListViewModel viewModel = new AllProblemsByContestIdListViewModel
            {
                Problems = this.problemsService.GetAllByContestIdViewModel<AllProblemsByContestIdViewModel>(contestId, problemId, 1, 100000),
                CurrentProblem = this.problemsService.GetById<CurrentProblemViewModel>(problemId),
                Submissions = this.submissionsService.GetTop5ByProblemIdAndUserId<Top5SubmissionsByUserIdAndProblemIdViewModel>(problemId, user.Id),
            };

            string contestName = this.contestsService.GetContestNameById(contestId);
            this.ViewData["Title"] = contestName;

            return this.View(viewModel);
        }
    }
}
