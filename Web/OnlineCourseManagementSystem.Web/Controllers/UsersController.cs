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
    using OnlineCourseManagementSystem.Web.ViewModels.Certificates;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
    using OnlineCourseManagementSystem.Web.ViewModels.Subscribers;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ICoursesService coursesService;
        private readonly ICertificatesService certificatesService;
        private readonly IExamsService examsService;
        private readonly ICompletitionsService completitionsService;
        private readonly ITagsService tagsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubscribersService subscribersService;
        private readonly ITownsService townsService;

        public UsersController(
            IUsersService usersService,
            ICoursesService coursesService,
            ICertificatesService certificatesService,
            IExamsService examsService,
            ICompletitionsService completitionsService,
            ITagsService tagsService,
            UserManager<ApplicationUser> userManager,
            ISubscribersService subscribersService,
            ITownsService townsService)
        {
            this.usersService = usersService;
            this.coursesService = coursesService;
            this.certificatesService = certificatesService;
            this.examsService = examsService;
            this.completitionsService = completitionsService;
            this.tagsService = tagsService;
            this.userManager = userManager;
            this.subscribersService = subscribersService;
            this.townsService = townsService;
        }

        [Breadcrumb("My Courses Info", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> CoursesInfo()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            CoursesInfoForCurrentUserViewModel viewModel = new CoursesInfoForCurrentUserViewModel
            {
                Certificates = this.certificatesService.GetAllByUserId<AllCertificatesByUserIdViewModel>(user.Id),
                FollowedCourses = this.coursesService.GetAllFollewedByUserId<AllFollowedCoursesByUserIdViewModel>(user.Id),
                CompletedCourses = this.coursesService.GetAllCompletedByUserId<AllCompletedCoursesByUserIdViewModel>(user.Id),
                FeaturedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
            };

            foreach (var course in viewModel.CompletedCourses)
            {
                course.CompletedLecturesCount = this.completitionsService.GetAllCompletitionsCountByCourseIdAndUserId(course.Id, user.Id);
                course.Tags = this.tagsService.GetAllByCourseId<AllTagsByCourseIdViewModel>(course.Id);
            }

            foreach (var course in viewModel.FollowedCourses)
            {
                course.CompletedLecturesCount = this.completitionsService.GetAllCompletitionsCountByCourseIdAndUserId(course.Id, user.Id);
                course.Tags = this.tagsService.GetAllByCourseId<AllTagsByCourseIdViewModel>(course.Id);
            }

            foreach (var certificate in viewModel.Certificates)
            {
                certificate.Grade = this.examsService.GetGradeByUserIdAndCourseId(user.Id, certificate.CourseId);
            }

            this.ViewData["CurrentUserHeading"] = "Messages";
            return this.View(viewModel);
        }

        [Breadcrumb("Manage Account", FromAction ="Index", FromController =typeof(HomeController))]
        public async Task<IActionResult> ManageAccountById()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            ManageAccountInputModel inputModel = this.usersService.GetById<ManageAccountInputModel>(user.Id);
            inputModel.Subscribed = this.subscribersService.CheckSubscribedByEmail(user.Email);

            inputModel.TownItems = this.townsService.GetAllAsSelectListItems();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageAccountById(ManageAccountInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);

                inputModel.Subscribed = this.subscribersService.CheckSubscribedByEmail(user.Email);
                inputModel = this.usersService.GetById<ManageAccountInputModel>(user.Id);
                inputModel.TownItems = this.townsService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            await this.usersService.UpdateAsync(inputModel);

            this.TempData["UpdatedAccount"] = "Successfully updated account";
            return this.RedirectToAction("ManageAccountById", "Users");
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.usersService.DeleteAsync(id);
            this.TempData["Message"] = "Successfully deleted user!";
            return this.RedirectToAction("AdminDashboard", "Dashboard");
        }

        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public IActionResult AllLecturers()
        {
            IEnumerable<UserViewModel> lecturers = this.usersService.GetAllLecturers<UserViewModel>();

            BreadcrumbNode dashboardNode = new MvcBreadcrumbNode("AdminDashboard", "Dashboard", "Dashboard");
            BreadcrumbNode allLecturersNode = new MvcBreadcrumbNode("AllLecturers", "Users", "All Lecturers")
            {
                Parent = dashboardNode,
            };

            this.ViewData["BreadcrumbNode"] = allLecturersNode;
            return this.View(lecturers);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AllStudents()
        {
            IEnumerable<UserViewModel> students = this.usersService.GetAllStudents<UserViewModel>();
            BreadcrumbNode dashboardNode = new MvcBreadcrumbNode("AdminDashboard", "Dashboard", "Dashboard");

            BreadcrumbNode allStudentsNode = new MvcBreadcrumbNode("AllStudents", "Users", "All Students")
            {
                Parent = dashboardNode,
            };

            this.ViewData["BreadcrumbNode"] = allStudentsNode;
            return this.View(students);
        }

        [Authorize]
        public IActionResult AllTeachers()
        {
            this.ViewData["CurrentUserHeading"] = "Messages";
            return this.View(this.usersService.GetAllLecturers<UserViewModel>());
        }
    }
}
