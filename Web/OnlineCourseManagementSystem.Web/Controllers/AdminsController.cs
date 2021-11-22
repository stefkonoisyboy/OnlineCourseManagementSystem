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
    using OnlineCourseManagementSystem.Web.ViewModels.Admins;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Events;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Posts;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class AdminsController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly IEventsService eventsService;
        private readonly ILecturesService lecturesService;
        private readonly IExamsService examsService;
        private readonly IPostsService postsService;
        private readonly IUsersService usersService;
        private readonly IAssignmentsService assignmentsService;
        private readonly ISubjectsService subjectsService;
        private readonly ITownsService townsService;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminsController(
            ICoursesService coursesService,
            IEventsService eventsService,
            ILecturesService lecturesService,
            IExamsService examsService,
            IPostsService postsService,
            IUsersService usersService,
            IAssignmentsService assignmentsService,
            ISubjectsService subjectsService,
            ITownsService townsService,
            UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.eventsService = eventsService;
            this.lecturesService = lecturesService;
            this.examsService = examsService;
            this.postsService = postsService;
            this.usersService = usersService;
            this.assignmentsService = assignmentsService;
            this.subjectsService = subjectsService;
            this.townsService = townsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Breadcrumb("Admin Actions", FromAction = "Index", FromController =typeof(HomeController))]
        public IActionResult AdminActions()
        {
            AdminItemsViewModel viewModel = new AdminItemsViewModel()
            {
                Courses = this.coursesService.GetAllByAdmin<CourseByAdminViewModel>(),
                Events = this.eventsService.GetAllByAdmin<EventByAdminViewModel>(),
                Lectures = this.lecturesService.GetAllByAdmin<LectureByAdminViewModel>(),
                Exams = this.examsService.GetAllByAdmin<ExamByAdminViewModel>(),
                Posts = this.postsService.GetAllByAdmin<PostByAdminViewModel>(),
                Assignments = this.assignmentsService.GetAllByAdmin<AssignmentByAdminViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            await this.assignmentsService.DeleteAssignment(id);
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public IActionResult EditAssignment(int id)
        {
            BreadcrumbNode adminActionsNode = new MvcBreadcrumbNode("AdminActions", "Admins", "Admin Actions");
            BreadcrumbNode editAssignmentNode = new MvcBreadcrumbNode("EditAssignment", "Admins", "Edit Assignemnt")
            {
                Parent = adminActionsNode,
            };

            this.ViewData["BreadcrumbNode"] = editAssignmentNode;
            EditAssignmentInputModel inputModel = this.assignmentsService.GetById<EditAssignmentInputModel>(id);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditAssignment(EditAssignmentInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.assignmentsService.UpdateAsync(inputModel);
            this.TempData["Message"] = "Successfully updated assignment!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await this.coursesService.DeleteAsync(id);
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditCourse(int id)
        {
            BreadcrumbNode adminActionsNode = new MvcBreadcrumbNode("AdminActions", "Admins", "Admin Actions");
            BreadcrumbNode editCourse = new MvcBreadcrumbNode("EditCourse", "Admins", "Edit Course")
            {
                Parent = adminActionsNode,
            };

            this.ViewData["BreadcrumbNode"] = editCourse;
            EditCourseAsAdminInputModel inputModel = new EditCourseAsAdminInputModel()
            {
                EditCourseInputModel = this.coursesService.GetById<EditCourseInputModel>(id),
                EditMetaInputModel = this.coursesService.GetById<EditMetaInputModel>(id),
            };

            inputModel.EditMetaInputModel.SubjectsItems = this.subjectsService.GetAllAsSelectListItems();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditCourse(EditCourseAsAdminInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.EditCourseInputModel = this.coursesService.GetById<EditCourseInputModel>(inputModel.EditMetaInputModel.Id);
                inputModel.EditMetaInputModel = this.coursesService.GetById<EditMetaInputModel>(inputModel.EditMetaInputModel.Id);
                inputModel.EditMetaInputModel.SubjectsItems = this.subjectsService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            await this.coursesService.UpdateAsync(inputModel.EditCourseInputModel);
            await this.coursesService.UpdateMetaAsync(inputModel.EditMetaInputModel);
            await this.coursesService.UpdateModifiedOnById(inputModel.EditMetaInputModel.Id);

            this.TempData["Message"] = "Successfully updated course!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveCourse(int id)
        {
            await this.coursesService.ApproveAsync(id);
            this.TempData["Message"] = "Successfully approved course!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await this.eventsService.DeleteAsync(id);
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditEvent(int id)
        {
            BreadcrumbNode adminActionsNode = new MvcBreadcrumbNode("AdminActions", "Admins", "Admin Actions");
            BreadcrumbNode editEventNode = new MvcBreadcrumbNode("EditEvent", "Admins", "Edit Event")
            {
                Parent = adminActionsNode,
            };

            this.ViewData["BreadcrumbNode"] = editEventNode;
            EditEventInputModel inputModel = this.eventsService.GetById<EditEventInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditEvent(EditEventInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.eventsService.UpdateAsync(inputModel);

            this.TempData["Message"] = "Successfully updated event!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveEvent(int id)
        {
            await this.eventsService.ApproveAsync(id);
            this.ViewData["Message"] = "Successfully approved event!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DisapproveEvent(int id)
        {
            await this.eventsService.DisapproveAsync(id);
            this.TempData["Message"] = "Successfully disapproved event!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeletePost(int id)
        {
            await this.postsService.DeleteAsync(id);
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditPost(int id)
        {
            BreadcrumbNode adminActionsNode = new MvcBreadcrumbNode("AdminActions", "Admins", "Admin Actions");
            BreadcrumbNode editPostNode = new MvcBreadcrumbNode("EditPost", "Admins", "Edit Post")
            {
                Parent = adminActionsNode,
            };
            EditPostInputModel inputModel = this.postsService.GetById<EditPostInputModel>(id);
            inputModel.CourseItems = this.coursesService.GetAllAsSelectListItems();
            this.ViewData["BreadcrumbNode"] = editPostNode;

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditPost(EditPostInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CourseItems = this.coursesService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            inputModel.AuthorId = user.Id;
            await this.postsService.UpdateAsync(inputModel);
            this.TempData["Message"] = "Successfully updated post!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            await this.lecturesService.DeleteAsync(id);
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditLecture(int id)
        {
            BreadcrumbNode adminActionsNode = new MvcBreadcrumbNode("AdminActions", "Admins", "Admin Actions");
            BreadcrumbNode editLecture = new MvcBreadcrumbNode("EditLecture", "Admins", "Edit Lecture")
            {
                Parent = adminActionsNode,
            };
            EditLectureInputModel inputModel = this.lecturesService.GetById<EditLectureInputModel>(id);
            inputModel.CoursesItems = this.coursesService.GetAllAsSelectListItems();
            this.ViewData["BreadcrumbNode"] = editLecture;
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditLecture(EditLectureInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CoursesItems = this.coursesService.GetAllAsSelectListItems();
            }

            await this.lecturesService.UpdateAsync(inputModel);
            await this.lecturesService.UpdateModifiedOnById(inputModel.Id);

            this.TempData["Message"] = "Successfully updated lecture!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteExam(int id)
        {
            await this.examsService.DeleteAsync(id);
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditExam(int id)
        {
            BreadcrumbNode adminActionsNode = new MvcBreadcrumbNode("AdminActions", "Admins", "Admin Actions");
            BreadcrumbNode editExamNode = new MvcBreadcrumbNode("EditExam", "Admins", "Edit Exam")
            {
                Parent = adminActionsNode,
            };

            this.ViewData["BreadcrumbNode"] = adminActionsNode;
            EditExamInputModel inputModel = this.examsService.GetById<EditExamInputModel>(id);
            inputModel.CourseItems = this.coursesService.GetAllAsSelectListItems();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditExam(EditExamInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CourseItems = this.coursesService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            await this.examsService.UpdateAsync(inputModel);
            this.TempData["Message"] = "Successfully updated exam!";
            return this.RedirectToAction("AdminActions", "Admins");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult ManageUserAccountById(string id)
        {
            ManageAccountInputModel inputModel = this.usersService.GetById<ManageAccountInputModel>(id);
            inputModel.TownItems = this.townsService.GetAllAsSelectListItems();
            BreadcrumbNode dashboardNode = new MvcBreadcrumbNode("AdminDasboard", "Admins", "Dashboard");
            BreadcrumbNode manageUserAccountNode = new MvcBreadcrumbNode("ManageUserAccountById", "Admins", "Manager User Account")
            {
                Parent = dashboardNode,
            };

            this.ViewData["BreadcrumbNode"] = manageUserAccountNode;
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ManageUserAccountById(ManageAccountInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel = this.usersService.GetById<ManageAccountInputModel>(inputModel.Id);
                inputModel.TownItems = this.townsService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            this.TempData["Message"] = "Successfully updated user!";

            await this.usersService.UpdateAsync(inputModel);
            return this.RedirectToAction("AdminDashboard", "Dashboard");
        }
    }
}
