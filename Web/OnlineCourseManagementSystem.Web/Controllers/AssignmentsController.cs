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
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

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
                Students = this.studentsService.GetAllAsSelectListItemsByCourse(id),
                CourseId = id,
            };

            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentUser", "Courses", "My Courses");
            BreadcrumbNode createassignmentNode = new MvcBreadcrumbNode("CreateAssignment", "Assignemnts", "Create Assignment")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = id },
            };

            this.ViewData["BreadcrumbNode"] = createassignmentNode;

            return this.View(createAssignmentInputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.LecturerRoleName)]

        public async Task<IActionResult> CreateAssignment(CreateAssignmentInputModel inputModel, int id)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Students = this.studentsService.GetAllAsSelectListItemsByCourse(id);
                return this.View(inputModel);
            }

            inputModel.CourseId = id;
            await this.assignmentsService.CreateAsync(inputModel);

            this.TempData["Message"] = "Succesfully created assignment";

            return this.RedirectToAction("AllCreated", "Assignments", new { Id = id });
        }

        [Authorize]
        [Breadcrumb("My Assignments", FromAction ="Index", FromController =typeof(HomeController))]
        public async Task<IActionResult> All()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllAssignmentsViewModel allAssignmentsViewModel = new AllAssignmentsViewModel
            {
                UnfinishedAssignments = this.assignmentsService.GetAllBy<AssignmentViewModel>(user.Id),
                FinishedAssignments = this.assignmentsService.GetAllFinishedBy<FinishedAssignmentViewModel>(user.Id),
            };

            return this.View(allAssignmentsViewModel);
        }

        [Authorize]
        public async Task<IActionResult> GetInfo(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AssignmentPageViewModel assignmentPageViewModel = this.assignmentsService
                .GetById<AssignmentPageViewModel>(id, user.Id);

            IEnumerable<FileAssignmentViewModel> resourceFiles = this.filesService.GetAllResourceFilesByAssignemt<FileAssignmentViewModel>(id, user.Id);
            IEnumerable<FileAssignmentViewModel> workFiles = this.filesService.GetAllUserSubmittedFilesForAssignment<FileAssignmentViewModel>(id, user.Id);

            if (resourceFiles.Any())
            {
                assignmentPageViewModel.ResourceFiles = resourceFiles.ToList();
            }

            if (workFiles.Any())
            {
                assignmentPageViewModel.WorkFiles = workFiles.ToList();
            }

            await this.assignmentsService.MarkAsSeen(id, user.Id);

            return this.View(assignmentPageViewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AllCreated(int id)
        {
            AllLectureAssignmetsByCourseViewModel assignmetViewModel = new AllLectureAssignmetsByCourseViewModel
            {
                CreatedAssignments = this.assignmentsService.GetAllBy<LectureAssignmentViewModel>(id),

                // CheckedAssignmets = this.assignmentsService.GetAllCheckedBy<LectureAssignmentViewModel>(id),
                CourseId = id,
            };

            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentUser", "Courses", "My Courses");
            MvcBreadcrumbNode mycreatedAssignmentsNode = new MvcBreadcrumbNode("AllCreated", "Assignments", "My Created Assignments")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = id, },
            };

            foreach (var assignment in assignmetViewModel.CreatedAssignments)
            {
                assignment.Users = this.assignmentsService.GetAllUsersForAssignment<AssignmentUserInfoViewModel>(assignment.AssignmentId);
            }

            this.ViewData["BreadcrumbNode"] = mycreatedAssignmentsNode;
            return this.View(assignmetViewModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            int courseId = await this.assignmentsService.DeleteAssignment(id);

            return this.RedirectToAction("AllCreated", "Assignments", new { Id = courseId });
        }

        public IActionResult Edit(int id)
        {
            EditAssignmentInputModel editAssignmentInputModel = this.assignmentsService.GetById<EditAssignmentInputModel>(id);

            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentLecture", "Courses", "My Courses");
            BreadcrumbNode mycreatedAssignmentsNode = new MvcBreadcrumbNode("AllCreated", "Assignments", "My Created Assignments")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = editAssignmentInputModel.CourseId, },
            };

            BreadcrumbNode editAssignmentNode = new MvcBreadcrumbNode("Edit", "Assignments", "Edit Assignment")
            {
                Parent = mycreatedAssignmentsNode,
            };

            this.ViewData["BreadcrumbNode"] = editAssignmentInputModel;
            return this.View(editAssignmentInputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Edit(EditAssignmentInputModel inputModel, int assignmentId, int courseId)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel = this.assignmentsService.GetById<EditAssignmentInputModel>(assignmentId);
                return this.View(inputModel);
            }

            inputModel.AssignmentId = assignmentId;
            inputModel.CourseId = courseId;
            await this.assignmentsService.UpdateAsync(inputModel);
            return this.RedirectToAction("AllCreated", "Assignments", new { Id = courseId});
        }

        // [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        // public IActionResult AllUsersForAssignment(int id)
        // {
        //    var usersAssignments = this.assignmentsService.GetAllUsersForAssignment<AssignmentUserInfoViewModel>(id);

        // this.TempData["AssignmentId"] = id;

        // return this.View(usersAssignments);
        // }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TurnIn(FilesToAssignmentInputModel inputModel, int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            inputModel.AssignmentId = id;
            inputModel.UserId = user.Id;
            await this.assignmentsService.TurnIn(inputModel);

            return this.RedirectToAction("GetInfo", "Assignments", new { Id = id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UndoTurnIn(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            await this.assignmentsService.UndoTurnIn(id, user.Id);

            return this.RedirectToAction("GetInfo", "Assignments", new { Id = id });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult MarkUserAssignment(int assignmentId, string userId)
        {
            MarkSubmittedAssignmentViewModel viewModel = this.assignmentsService.GetById<MarkSubmittedAssignmentViewModel>(assignmentId, userId);
            viewModel.AssignmentInfo = this.assignmentsService.GetById<AssignmentInfoViewModel>(assignmentId, userId);
            viewModel.AssignmentInfo.WorkFiles = this.filesService.GetAllUserSubmittedFilesForAssignment<FileAssignmentViewModel>(assignmentId, userId);
            viewModel.AssignmentInfo.ResourceFiles = this.filesService.GetAllResourceFilesByAssignemt<FileAssignmentViewModel>(assignmentId, userId);

            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentUser", "Courses", "My Courses");
            BreadcrumbNode mycreatedAssignmentsNode = new MvcBreadcrumbNode("AllCreated", "Assignments", "My Created Assignments")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = viewModel.CourseId, },
            };
            BreadcrumbNode markUserAssignment = new MvcBreadcrumbNode("MarkUserAssignment", "Assignments", "Mark User Assignment")
            {
                Parent = mycreatedAssignmentsNode,
            };

            this.ViewData["BreadcrumbNode"] = markUserAssignment;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> MarkUserAssignment(MarkSubmittedAssignmentViewModel viewModel, string userId)
        {
            try
            {
                viewModel.InputModel.UserId = userId;
                viewModel.InputModel.AssignmentId = viewModel.AssignmentId;
                int courseId = await this.assignmentsService.MarkSubmittedAssignment(viewModel.InputModel);

                return this.RedirectToAction("AllCreated", "Assignments", new { Id = courseId });
            }
            catch (Exception e)
            {
                viewModel = this.assignmentsService.GetById<MarkSubmittedAssignmentViewModel>(viewModel.AssignmentId, userId);
                viewModel.AssignmentInfo = this.assignmentsService.GetById<AssignmentInfoViewModel>(viewModel.AssignmentId, userId);
                viewModel.AssignmentInfo.WorkFiles = this.filesService.GetAllUserSubmittedFilesForAssignment<FileAssignmentViewModel>(viewModel.AssignmentId, userId);
                viewModel.AssignmentInfo.ResourceFiles = this.filesService.GetAllResourceFilesByAssignemt<FileAssignmentViewModel>(viewModel.AssignmentId, userId);
                this.TempData["ErrorPoints"] = e.Message;
                return this.RedirectToAction("MarkUserAssignment", "Assignments", new { AssignmentId = viewModel.AssignmentId, UserId = userId });
            }
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult EditCheckedAssignment(int assignmentId, string userId)
        {
            EditCheckedUserAssignmentViewModel viewModel = this.assignmentsService.GetCheckedBy<EditCheckedUserAssignmentViewModel>(assignmentId, userId);
            viewModel.InputModel = this.assignmentsService.GetCheckedBy<EditCheckedAssignmentInputModel>(assignmentId, userId);
            viewModel.AssignmentInfo = this.assignmentsService.GetById<AssignmentInfoViewModel>(assignmentId, userId);
            viewModel.AssignmentInfo.ResourceFiles = this.filesService.GetAllResourceFilesByAssignemt<FileAssignmentViewModel>(assignmentId, userId);
            viewModel.AssignmentInfo.WorkFiles = this.filesService.GetAllUserSubmittedFilesForAssignment<FileAssignmentViewModel>(assignmentId, userId);

            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentUser", "Courses", "My Courses");

            BreadcrumbNode mycreatedAssignmentsNode = new MvcBreadcrumbNode("AllCreated", "Assignments", "My Created Assignments")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = viewModel.CourseId},
            };

            BreadcrumbNode markUserAssignment = new MvcBreadcrumbNode("MarkUserAssignment", "Assignments", "Mark User Assignment")
            {
                Parent = mycreatedAssignmentsNode,
            };

            this.ViewData["BreadcrumbNode"] = markUserAssignment;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> EditCheckedAssignment(EditCheckedUserAssignmentViewModel viewModel, string userId, int courseId)
        {
            try
            {
                viewModel.InputModel.AssignmentId = viewModel.AssignmentId;
                viewModel.InputModel.UserId = userId;
                await this.assignmentsService.UpdateCheckedAsync(viewModel.InputModel);
                this.TempData["Message"] = "Successfully editted marked Assignment!";

                return this.RedirectToAction("AllCreated", "Assignments", new { Id = courseId });
            }
            catch (Exception e)
            {
                viewModel.InputModel = this.assignmentsService.GetCheckedBy<EditCheckedAssignmentInputModel>(viewModel.AssignmentId, userId);
                viewModel.AssignmentInfo = this.assignmentsService.GetById<AssignmentInfoViewModel>(viewModel.AssignmentId, userId);
                viewModel.AssignmentInfo.ResourceFiles = this.filesService.GetAllResourceFilesByAssignemt<FileAssignmentViewModel>(viewModel.AssignmentId, userId);
                viewModel.AssignmentInfo.WorkFiles = this.filesService.GetAllUserSubmittedFilesForAssignment<FileAssignmentViewModel>(viewModel.AssignmentId, userId);

                this.TempData["ErrorPoints"] = e.Message;
                return this.RedirectToAction("EditCheckedAssignment", "Assignments", new { AssignmentId = viewModel.AssignmentId, UserId = userId });
            }
        }

        [Authorize]
        public async Task<IActionResult> CurrentUserAllAssignmentsByCourse(int courseId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentUser", "Courses", "My Courses");

            BreadcrumbNode myassignmentsByCourseNode = new MvcBreadcrumbNode("CurrentUserAllAssignmentsByCourse", "AssignmentsController", "My Assignments By Course")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = courseId, },
            };

            AllAssignmentsViewModel allAssignmentsViewModel = new AllAssignmentsViewModel
            {
                UnfinishedAssignments = this.assignmentsService.GetAllByCourseAndUser<AssignmentViewModel>(courseId, user.Id),
                FinishedAssignments = this.assignmentsService.GetAllFinishedByCourseAndUser<FinishedAssignmentViewModel>(courseId, user.Id),
            };

            this.ViewData["BreadcrumbNode"] = myassignmentsByCourseNode;
            return this.View(allAssignmentsViewModel);
        }

        [Breadcrumb("My Assignments", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> AllCreatedByLecturer()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            AllLectureAssignmentsViewModel viewModel = new AllLectureAssignmentsViewModel()
            {
                CreatedAssignments = this.assignmentsService.GetAllByLecturer<LectureAssignmentViewModel>(user.Id),
            };

            foreach (var assignment in viewModel.CreatedAssignments)
            {
                assignment.Users = this.assignmentsService.GetAllUsersForAssignment<AssignmentUserInfoViewModel>(assignment.AssignmentId);
            }

            return this.View(viewModel);
        }

        // public IActionResult AllCheckedUsersForAssignment(int id)
        // {
        //    var userAssignments = this.assignmentsService.GetAllCheckedUserAssignments<CheckedUserAssignmentsViewModel>(id);

        // return this.View(userAssignments);
        // }
    }
}
