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
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class LecturesController : Controller
    {
        private readonly ILecturesService lecturesService;
        private readonly IFilesService fileService;
        private readonly ICoursesService coursesService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public LecturesController(
            ILecturesService lecturesService,
            IFilesService fileService,
            ICoursesService coursesService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.lecturesService = lecturesService;
            this.fileService = fileService;
            this.coursesService = coursesService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLectureInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CoursesItems = this.coursesService.GetAllAsSelectListItemsByCreatorId(user.Id);
                input.CreatorId = user.Id;
                return this.View(input);
            }

            input.CreatorId = user.Id;
            await this.lecturesService.CreateAsync(input);
            this.TempData["Message"] = "Lecture created successfully!";

            return this.RedirectToAction(nameof(this.AllLecturesByCreatorId));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            EditLectureInputModel input = this.lecturesService.GetById<EditLectureInputModel>(id);
            input.CoursesItems = this.coursesService.GetAllAsSelectListItemsByCreatorId(user.Id);
            input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
            input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditLectureInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CoursesItems = this.coursesService.GetAllAsSelectListItemsByCreatorId(user.Id);
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();

                this.ViewData["CurrentUserHeading"] = "Messages";

                return this.View(input);
            }

            input.Id = id;
            await this.lecturesService.UpdateAsync(input);
            this.TempData["Message"] = "Lecture updated successfully!";

            return this.RedirectToAction(nameof(this.AllLecturesByCreatorId));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> AddWord(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AddFileToLectureInputModel input = new AddFileToLectureInputModel
            {
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddWord(AddFileToLectureInputModel input, int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                return this.View(input);
            }

            this.TempData["Message"] = "Word Document added successfully to the Lecture!";
            input.LectureId = id;
            input.UserId = user.Id;
            int courseId = this.lecturesService.GetCourseIdByLectureId(id);
            await this.lecturesService.AddWordFileAsync(input);
            await this.lecturesService.UpdateModifiedOnById(id);

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.Redirect($"/Courses/ById/{courseId}");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> AddPresentation(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AddFileToLectureInputModel input = new AddFileToLectureInputModel
            {
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddPresentation(AddFileToLectureInputModel input, int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                return this.View(input);
            }

            this.TempData["Message"] = "Presentation added successfully to the Lecture!";
            input.LectureId = id;
            input.UserId = user.Id;
            int courseId = this.lecturesService.GetCourseIdByLectureId(id);
            await this.lecturesService.AddPresentationFileAsync(input);
            await this.lecturesService.UpdateModifiedOnById(id);

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.Redirect($"/Courses/ById/{courseId}");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> AddVideo(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AddVideoToLectureInputModel input = new AddVideoToLectureInputModel
            {
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddVideo(AddVideoToLectureInputModel input, int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                return this.View(input);
            }

            this.TempData["Message"] = "Video added successfully to the Lecture!";
            input.LectureId = id;
            input.UserId = user.Id;
            int courseId = this.lecturesService.GetCourseIdByLectureId(id);
            await this.lecturesService.AddVideoAsync(input);
            await this.lecturesService.UpdateModifiedOnById(id);

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.Redirect($"/Courses/ById/{courseId}");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> AllLecturesByCreatorId(int id = 1)
        {
            const int ItemsPerPage = 3;
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllLecturesByCreatorIdListViewModel viewModel = new AllLecturesByCreatorIdListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                LecturesByCreatorIdCount = this.lecturesService.GetLecturesCountByCreatorId(user.Id),
                Lectures = this.lecturesService.GetAllByCreatorId<AllLecturesByCreatorIdViewModel>(id, user.Id, ItemsPerPage),
                RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                Input = new CreateLectureInputModel
                {
                    CoursesItems = this.coursesService.GetAllAsSelectListItemsByCreatorId(user.Id),
                    CreatorId = user.Id,
                },
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllVideosById(int id)
        {
            AllVideosByIdListViewModel viewModel = new AllVideosByIdListViewModel
            {
                Videos = this.lecturesService.GetAllVideosById<AllVideosByIdViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
