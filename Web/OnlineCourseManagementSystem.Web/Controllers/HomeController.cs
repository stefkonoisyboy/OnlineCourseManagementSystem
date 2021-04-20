namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IAssignmentsService assignmentsService, UserManager<ApplicationUser> userManager)
        {
            this.assignmentsService = assignmentsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if(this.User.IsInRole(GlobalConstants.StudentRoleName))
            {
                HomeViewModel homeViewModel = new HomeViewModel
                {
                    AssignmentsCount = this.assignmentsService.GetAllBy<AssignmentViewModel>(user.Id).Count(),
                };

                return this.View(homeViewModel);
            }
            else
            {
                return this.View();
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
