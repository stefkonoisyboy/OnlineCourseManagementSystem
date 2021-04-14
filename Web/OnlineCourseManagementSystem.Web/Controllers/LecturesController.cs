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
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;

    public class LecturesController : Controller
    {
        private readonly ILecturesService lecturesService;
        private readonly IFileService fileService;
        private readonly UserManager<ApplicationUser> userManager;

        public LecturesController(
            ILecturesService lecturesService,
            IFileService fileService,
            UserManager<ApplicationUser> userManager)
        {
            this.lecturesService = lecturesService;
            this.fileService = fileService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create(int id)
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLectureInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            input.CourseId = id;
            await this.lecturesService.CreateAsync(input);
            this.TempData["Message"] = "Lecture created successfully!";

            return this.Redirect($"/Courses/ById/{id}");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AddWord(int id)
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddWord(AddFileToLectureInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            this.TempData["Message"] = "Word Document added successfully to the Lecture!";
            input.LectureId = id;
            input.UserId = user.Id;
            await this.lecturesService.AddWordFileAsync(input);

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AddPresentation(int id)
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddPresentation(AddFileToLectureInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            this.TempData["Message"] = "Presentation added successfully to the Lecture!";
            input.LectureId = id;
            input.UserId = user.Id;
            await this.lecturesService.AddPresentationFileAsync(input);

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AddVideo(int id)
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddVideo(AddVideoToLectureInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            this.TempData["Message"] = "Video added successfully to the Lecture!";
            input.LectureId = id;
            input.UserId = user.Id;
            await this.lecturesService.AddVideoAsync(input);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult AllFilesById(int id)
        {
            AllFilesByIdListViewModel viewModel = new AllFilesByIdListViewModel
            {
                Files = this.fileService.GetAllById<AllFilesByIdViewModel>(id),
            };

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
