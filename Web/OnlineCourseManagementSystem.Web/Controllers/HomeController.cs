namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.ContactMessages;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Home;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;

    public class HomeController : BaseController
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly IOrdersService ordersService;
        private readonly ICoursesService coursesService;
        private readonly IReviewsService reviewsService;
        private readonly IUsersService usersService;
        private readonly IContactMessagesService contactMessagesService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            IAssignmentsService assignmentsService,
            IOrdersService ordersService,
            ICoursesService coursesService,
            IReviewsService reviewsService,
            IUsersService usersService,
            IContactMessagesService contactMessagesService,
            UserManager<ApplicationUser> userManager)
        {
            this.assignmentsService = assignmentsService;
            this.ordersService = ordersService;
            this.coursesService = coursesService;
            this.reviewsService = reviewsService;
            this.usersService = usersService;
            this.contactMessagesService = contactMessagesService;
            this.userManager = userManager;
        }

        [Authorize]
        [DefaultBreadcrumb(" Home ")]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (this.User.IsInRole(GlobalConstants.StudentRoleName))
            {
                HomeViewModel homeViewModel = new HomeViewModel
                {
                    AssignmentsCount = this.assignmentsService.GetAllBy<AssignmentViewModel>(user.Id).Count(),
                    CoursesInCartCount = this.ordersService.CoursesInCartCount(user.Id),
                    Reviews = this.reviewsService.GetTop3Recent<AllReviewsByCourseIdViewModel>(),
                    FeaturedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                    CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                    UserId = user.Id,
                };

                return this.View(homeViewModel);
            }
            else
            {
                HomeViewModel homeViewModel = new HomeViewModel
                {
                    Reviews = this.reviewsService.GetTop3Recent<AllReviewsByCourseIdViewModel>(),
                    FeaturedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                    CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                };
                return this.View(homeViewModel);
            }
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [Breadcrumb(" Contact ", FromAction = "Index")]
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(CreateContactMessageInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.contactMessagesService.CreateAsync(inputModel);
            this.TempData["Success-ContactMessage-Create"] = "Successfully created contact message!";

            return this.View();
        }

        [Breadcrumb(" About ", FromAction = "Index")]
        public IActionResult About()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
