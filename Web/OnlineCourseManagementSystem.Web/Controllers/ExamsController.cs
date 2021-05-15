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
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;

    public class ExamsController : Controller
    {
        private readonly IExamsService examsService;
        private readonly ICoursesService coursesService;
        private readonly ILecturersService lecturersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExamsController(
            IExamsService examsService,
            ICoursesService coursesService,
            ILecturersService lecturersService,
            UserManager<ApplicationUser> userManager)
        {
            this.examsService = examsService;
            this.coursesService = coursesService;
            this.lecturersService = lecturersService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create()
        {
            CreateExamInputModel input = new CreateExamInputModel
            {
                CourseItems = this.coursesService.GetAllAsSelectListItems(),
            };

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateExamInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseItems = this.coursesService.GetAllAsSelectListItems();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.LecturerId = user.Id;
            await this.examsService.CreateAsync(input);
            this.TempData["Message"] = "Exam successfully created!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Edit(int id)
        {
            EditExamInputModel input = this.examsService.GetById<EditExamInputModel>(id);
            input.CourseItems = this.coursesService.GetAllAsSelectListItems();

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExamInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseItems = this.coursesService.GetAllAsSelectListItems();
                return this.View(input);
            }

            await this.examsService.UpdateAsync(input);
            this.TempData["Message"] = "Exam successfully updated!";

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.examsService.DeleteAsync(id);
            this.TempData["Message"] = "Exam successfully deleted!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult All()
        {
            AllExamsListViewModel viewModel = new AllExamsListViewModel
            {
                Exams = this.examsService.GetAll<AllExamsViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Details(int id)
        {
            ExamDetailsViewModel viewModel = this.examsService.GetById<ExamDetailsViewModel>(id);

            return this.View(viewModel);
        }
    }
}
