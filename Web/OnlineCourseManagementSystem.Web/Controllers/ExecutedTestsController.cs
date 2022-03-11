using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseManagementSystem.Services.Data;
using OnlineCourseManagementSystem.Web.ViewModels.ExecutedTests;
using SmartBreadcrumbs.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Web.Controllers
{
    public class ExecutedTestsController : Controller
    {
        private readonly IExecutedTestsService executedTestsService;
        private readonly ISubmissionsService submissionsService;
        private readonly IProblemsService problemsService;

        public ExecutedTestsController(
            IExecutedTestsService executedTestsService,
            ISubmissionsService submissionsService,
            IProblemsService problemsService)
        {
            this.executedTestsService = executedTestsService;
            this.submissionsService = submissionsService;
            this.problemsService = problemsService;
        }

        [Authorize]
        public IActionResult AllBySubmissionId(int submissionId)
        {
            int problemId = this.submissionsService.GetProblemIdBySubmissionId(submissionId);
            int contestId = this.problemsService.GetContestIdByProblemId(problemId);

            BreadcrumbNode parentNode = new MvcBreadcrumbNode("AllByContestId", "Problems", "Problems")
            {
                RouteValues = new { problemId = problemId, contestId = contestId, },
            };

            BreadcrumbNode childNode = new MvcBreadcrumbNode("AllBySubmissionId", "ExecutedTests", "Submission Details")
            {
                Parent = parentNode,
                RouteValues = new { submissionId = submissionId },
            };

            this.ViewData["BreadcrumbNode"] = childNode;

            IEnumerable<AllExecutedTestsBySubmissionIdViewModel> viewModel = this.executedTestsService.GetAllBySubmission<AllExecutedTestsBySubmissionIdViewModel>(submissionId);

            return this.View(viewModel);
        }
    }
}
