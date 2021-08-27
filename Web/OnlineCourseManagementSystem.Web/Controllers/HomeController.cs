namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Home;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class HomeController : BaseController
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly IOrdersService ordersService;
        private readonly ICoursesService coursesService;
        private readonly IReviewsService reviewsService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            IAssignmentsService assignmentsService,
            IOrdersService ordersService,
            ICoursesService coursesService,
            IReviewsService reviewsService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.assignmentsService = assignmentsService;
            this.ordersService = ordersService;
            this.coursesService = coursesService;
            this.reviewsService = reviewsService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize]
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
