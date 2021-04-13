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
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;

    public class AssignmentsController : Controller
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly IStudentsService studentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public AssignmentsController(IAssignmentsService assignmentsService, IStudentsService studentsService, UserManager<ApplicationUser> userManager)
        {
            this.assignmentsService = assignmentsService;
            this.studentsService = studentsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult CreateAssignment(CreateAssignmentInputModel inputModel, int id)
        {
            inputModel.Students = this.studentsService.GetAllAsSelectListItems(id);
            inputModel.CourseId = id;
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.LecturerRoleName)]

        public async Task<IActionResult> CreateAssignment(CreateAssignmentInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Students = this.studentsService.GetAllAsSelectListItems(inputModel.CourseId);
            }

            await this.assignmentsService.CreateAsync(inputModel);

            return this.View(inputModel);
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            var assignments = this.assignmentsService.GetAllBy<AssignmentViewModel>(user.Id);

            return this.View(assignments);
        }

        public IActionResult GetInfo(int id)
        {
            AssignmentPageViewModel assignmentPageViewModel = this.assignmentsService
                .GetBy<AssignmentPageViewModel>(id);

            this.assignmentsService.MarkAsSeen(id);

            return this.View(assignmentPageViewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllFinished()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            var assignments = this.assignmentsService.GetAllFinishedBy<AssignmentViewModel>(user.Id);

            return this.View(assignments);
        }
    }
}
