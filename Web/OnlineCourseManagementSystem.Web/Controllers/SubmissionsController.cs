namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.Scripting;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Submissions;

    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;
        private readonly IProblemsService problemsService;
        private readonly IWebHostEnvironment webEnv;
        private readonly UserManager<ApplicationUser> userManager;

        public SubmissionsController(
            ISubmissionsService submissionsService,
            IProblemsService problemsService,
            IWebHostEnvironment webEnv,
            UserManager<ApplicationUser> userManager)
        {
            this.submissionsService = submissionsService;
            this.problemsService = problemsService;
            this.webEnv = webEnv;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(int problemId, CreateSubmissionInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            int contestId = this.problemsService.GetContestIdByProblemId(problemId);

            input.ContestId = contestId;
            input.ProblemId = problemId;
            input.UserId = user.Id;

            try
            {
                await this.submissionsService.CreateAsync(input, this.webEnv.WebRootPath, this.webEnv.WebRootPath);
            }
            catch (Exception)
            {
            }

            this.TempData["Message"] = "You submitted your code successfully!";

            return this.RedirectToAction("AllByContestId", "Problems", new { problemId = problemId, contestId = contestId });
        }
    }
}
