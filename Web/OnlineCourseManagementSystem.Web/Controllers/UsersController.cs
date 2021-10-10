﻿namespace OnlineCourseManagementSystem.Web.Controllers
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
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

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
        private readonly ITownsService townsService;

        public UsersController(
            IUsersService usersService,
            ICoursesService coursesService,
            ICertificatesService certificatesService,
            IExamsService examsService,
            ICompletitionsService completitionsService,
            ITagsService tagsService,
            UserManager<ApplicationUser> userManager,
            ITownsService townsService)
        {
            this.usersService = usersService;
            this.coursesService = coursesService;
            this.certificatesService = certificatesService;
            this.examsService = examsService;
            this.completitionsService = completitionsService;
            this.tagsService = tagsService;
            this.userManager = userManager;
            this.townsService = townsService;
        }

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

            return this.View(viewModel);
        }

        public async Task<IActionResult> ManageAccountById()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            ManageAccountInputModel inputModel = this.usersService.GetById<ManageAccountInputModel>(user.Id);
            inputModel.TownItems = this.townsService.GetAllAsSelectListItems();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageAccountById(ManageAccountInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);

                inputModel = this.usersService.GetById<ManageAccountInputModel>(user.Id);
                inputModel.TownItems = this.townsService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            await this.usersService.UpdateAsync(inputModel);
            this.TempData["UpdatedAccount"] = "Successfully updated account";
            return this.RedirectToAction("ManageAccountById", "Users");
        }
    }
}
