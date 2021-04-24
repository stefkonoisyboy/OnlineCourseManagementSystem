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
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class AssignmentsController : Controller
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly IStudentsService studentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFilesService filesService;

        public AssignmentsController(IAssignmentsService assignmentsService, IStudentsService studentsService, UserManager<ApplicationUser> userManager, IFilesService filesService)
        {
            this.assignmentsService = assignmentsService;
            this.studentsService = studentsService;
            this.userManager = userManager;
            this.filesService = filesService;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult CreateAssignment(int id)
        {
            CreateAssignmentInputModel createAssignmentInputModel = new CreateAssignmentInputModel
            {
                Students = this.studentsService.GetAllAsSelectListItems(id),
                CourseId = id,
            };

            return this.View(createAssignmentInputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.LecturerRoleName)]

        public async Task<IActionResult> CreateAssignment(CreateAssignmentInputModel inputModel, int id)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Students = this.studentsService.GetAllAsSelectListItems(id);
                return this.View(inputModel);
            }

            inputModel.CourseId = id;
            await this.assignmentsService.CreateAsync(inputModel);

            this.TempData["CreatedAssignment"] = "Succesfully created assignment";

            return this.RedirectToAction("AllCreated", "Assignments", new { Id = id });
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllAssignmentsViewModel allAssignmentsViewModel = new AllAssignmentsViewModel
            {
                UnFinihedAssignements = this.assignmentsService.GetAllBy<AssignmentViewModel>(user.Id),
                FinishedAssignments = this.assignmentsService.GetAllFinishedBy<FinishedAssignmentViewModel>(user.Id),
            };

            return this.View(allAssignmentsViewModel);
        }

        [Authorize]
        public async Task<IActionResult> GetInfo(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AssignmentPageViewModel assignmentPageViewModel = this.assignmentsService
                .GetById<AssignmentPageViewModel>(id);
            assignmentPageViewModel.Files = this.filesService.GetAllByUserAndAssignment<FileAssignmentViewModel>(id, user.Id);
            await this.assignmentsService.MarkAsSeen(id, user.Id);

            return this.View(assignmentPageViewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllFinished()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            var assignments = this.assignmentsService.GetAllFinishedBy<AssignmentViewModel>(user.Id);

            return this.View(assignments);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AllCreated(int id)
        {
            var assignments = this.assignmentsService.GetAllBy<CreatedAssignmentsViewModel>(id);

            return this.View(assignments);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            int courseId = await this.assignmentsService.DeleteAssignment(id);

            return this.RedirectToAction("AllCreated", "Assignments", new { Id = courseId });
        }

        public IActionResult Edit(int id)
        {
            EditAssignmentInputModel editAssignmentInputModel = this.assignmentsService.GetById<EditAssignmentInputModel>(id);

            return this.View(editAssignmentInputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Edit(EditAssignmentInputModel inputModel, int id)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel = this.assignmentsService.GetById<EditAssignmentInputModel>(id);
            }

            await this.assignmentsService.UpdateAsync(inputModel);

            return this.RedirectToAction("AllCreated", "Assignments", new { Id = inputModel.CourseId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AllUsersForAssignment(int id)
        {
            var usersAssignments = this.assignmentsService.GetAllUsersForAssignment<AssignmentUserInfoViewModel>(id);

            this.TempData["AssignmentId"] = id;

            return this.View(usersAssignments);
        }

        [Authorize]
        public async Task<IActionResult> TurnIn(FilesToAssignmentInputModel inputModel, int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            inputModel.AssignmentId = id;
            inputModel.UserId = user.Id;
            await this.assignmentsService.TurnIn(inputModel);

            return this.RedirectToAction("All", "Assignments", new { Id = id });
        }

        public IActionResult MarkUserAssignment(string id)
        {
            int assignmentId = (int)this.TempData["AssignmentId"];
            this.TempData["AssignmentId"] = assignmentId;
            MarkSubmittedAssignmentViewModel viewModel = new MarkSubmittedAssignmentViewModel
            {
                Files = this.filesService.GetAllUserSubmittedFilesForAssignment<FileAssignmentViewModel>(assignmentId, id),
                UserId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> MarkUserAssignment(MarkSubmittedAssignmentViewModel viewModel, string id)
        {
            if (!this.ModelState.IsValid)
            {
                int assignmentId = (int)this.TempData["AssignmentId"];
                viewModel.Files = this.filesService.GetAllUserSubmittedFilesForAssignment<FileAssignmentViewModel>(assignmentId, id);
                viewModel.UserId = id;

                return this.View(viewModel);
            }

            viewModel.InputModel.UserId = id;
            viewModel.InputModel.AssignmentId = (int)this.TempData["AssignmentId"];
            int courseId = await this.assignmentsService.MarkSubmittedAssignment(viewModel.InputModel);

            return this.RedirectToAction("AllCreated", "Assignments", new { Id = courseId });
        }
    }
}
