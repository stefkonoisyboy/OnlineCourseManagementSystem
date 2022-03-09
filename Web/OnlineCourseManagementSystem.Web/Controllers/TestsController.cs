namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Tests;
    using SmartBreadcrumbs.Nodes;

    public class TestsController : Controller
    {
        private readonly ITestsService testsService;
        private readonly IProblemsService problemsService;

        public TestsController(ITestsService testsService, IProblemsService problemsService)
        {
            this.testsService = testsService;
            this.problemsService = problemsService;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create(int problemId)
        {
            int contestId = this.problemsService.GetContestIdByProblemId(problemId);
            BreadcrumbNode parentNode = new MvcBreadcrumbNode("AllByContestId", "Problems", "Problems")
            {
                RouteValues = new { problemId = problemId, contestId = contestId, },
            };

            BreadcrumbNode childNode = new MvcBreadcrumbNode("Create", "Tests", "Add Test")
            {
                Parent = parentNode,
                RouteValues = new { problemId = problemId },
            };

            this.ViewData["BreadcrumbNode"] = childNode;

            CreateTestInputModel input = new CreateTestInputModel
            {
                ProblemId = problemId,
            };
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(int problemId, CreateTestInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.ProblemId = problemId;
                return this.View(input);
            }

            input.ProblemId = problemId;
            await this.testsService.CreateAsync(input);
            this.TempData["Message"] = "Test added successfully to the problem!";

            int contestId = this.problemsService.GetContestIdByProblemId(problemId);

            return this.RedirectToAction("AllByContestId", "Problems", new { problemId = problemId, contestId = contestId, });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AllByProblemId(int problemId)
        {
            int contestId = this.problemsService.GetContestIdByProblemId(problemId);
            BreadcrumbNode parentNode = new MvcBreadcrumbNode("AllByContestId", "Problems", "Problems")
            {
                RouteValues = new { problemId = problemId, contestId = contestId, },
            };

            BreadcrumbNode childNode = new MvcBreadcrumbNode("AllByProblemId", "Tests", "All Tests by problem")
            {
                Parent = parentNode,
                RouteValues = new { problemId = problemId },
            };

            this.ViewData["BreadcrumbNode"] = childNode;

            IEnumerable<EditTestInputModel> viewModel = this.testsService.GetAllByProblemId<EditTestInputModel>(problemId);

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Update(int id)
        {
            EditTestInputModel input = this.testsService.GetById<EditTestInputModel>(id);
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Update(int id, EditTestInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.testsService.UpdateAsync(input);
            this.TempData["Message"] = "Test updated successfully!";

            return this.RedirectToAction("AllByProblemId", "Tests", new { problemId = input.ProblemId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Delete(int id, int problemId)
        {
            await this.testsService.DeleteAsync(id);
            this.TempData["Message"] = "Test deleted successfully!";

            int contestId = this.problemsService.GetContestIdByProblemId(problemId);

            return this.RedirectToAction("AllByContestId", "Problems", new { problemId = problemId, contestId = contestId, });
        }
    }
}
