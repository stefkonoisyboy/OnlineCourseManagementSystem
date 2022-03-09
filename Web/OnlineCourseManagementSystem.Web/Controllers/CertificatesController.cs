namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Certificates;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;

    public class CertificatesController : Controller
    {
        private readonly ICertificatesService certificatesService;
        private readonly ICoursesService coursesService;
        private readonly IUsersService usersService;
        private readonly IExamsService examsService;
        private readonly ILecturesService lecturesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CertificatesController(
            ICertificatesService certificatesService,
            ICoursesService coursesService,
            IUsersService usersService,
            IExamsService examsService,
            ILecturesService lecturesService,
            UserManager<ApplicationUser> userManager)
        {
            this.certificatesService = certificatesService;
            this.coursesService = coursesService;
            this.usersService = usersService;
            this.examsService = examsService;
            this.lecturesService = lecturesService;
            this.userManager = userManager;
        }

        [Authorize]
        [Breadcrumb("My Certificate", FromAction = "CoursesInfo", FromController = typeof(UsersController))]
        public async Task<IActionResult> ByCurrentUserAndCourse(int courseId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            CertificateByUserIdAndCourseIdViewModel viewModel = this.certificatesService.GetByUserIdAndCourseId<CertificateByUserIdAndCourseIdViewModel>(user.Id, courseId);
            viewModel.FeaturedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
            viewModel.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
            viewModel.Grade = this.examsService.GetGradeByUserIdAndCourseId(user.Id, courseId);
            viewModel.Lectures = this.lecturesService.GetAllById<AllLecturesByIdViewModel>(courseId, 1, 100000);
            int examId = this.examsService.GetExamIdByUserIdAndCourseId(user.Id, courseId);

            if (viewModel.Grade < 5.00)
            {
                this.TempData["Alert"] = "You need to get at least 5.00 on the final exam in order to get your certification!";
                return this.RedirectToAction("Review", "Exams", new { id = examId });
            }

            this.ViewData["CurrentUserHeading"] = "Messages";
            return this.View(viewModel);
        }
    }
}
