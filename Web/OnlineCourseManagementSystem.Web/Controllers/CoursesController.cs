namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;

    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly ITagsService tagsService;
        private readonly ISubjectsService subjectsService;
        private readonly ILecturersService lecturersService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            ICoursesService coursesService,
            ITagsService tagsService,
            ISubjectsService subjectsService,
            ILecturersService lecturersService,
            UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.tagsService = tagsService;
            this.subjectsService = subjectsService;
            this.lecturersService = lecturersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult ById(int id)
        {
            CourseByIdViewModel viewModel = this.coursesService.GetById<CourseByIdViewModel>(id);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult All()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAll<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AllUnapproved()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllUnapproved<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllByUser(string id)
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllByUser<AllCoursesViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllUpcoming()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllUpcoming<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllPast()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllPast<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllActive()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllActive<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AllByTag(SearchByTagInputModel input)
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllByTag<AllCoursesViewModel>(input),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create()
        {
            CreateCourseInputModel input = new CreateCourseInputModel
            {
                SubjectsItems = this.subjectsService.GetAllAsSelectListItems(),
                TagItems = this.tagsService.GetAllAsSelectListItems(),
                LecturerItems = this.lecturersService.GetAllAsSelectListItems(),
            };

            return this.View(input);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Create(CreateCourseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.SubjectsItems = this.subjectsService.GetAllAsSelectListItems();
                input.TagItems = this.tagsService.GetAllAsSelectListItems();
                input.LecturerItems = this.lecturersService.GetAllAsSelectListItems();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            await this.coursesService.CreateAsync(input);
            this.TempData["Message"] = "Course is created successfully!";

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            //EditCourseInputModel input = new EditCourseInputModel
            //{
            //    Id = id,
            //    Name = this.coursesService.GetNameById(id),
            //    Description = this.coursesService.GetDescriptionById(id),
            //    Price = this.coursesService.GetPriceById(id),
            //    StartDate = this.coursesService.GetStartDateById(id),
            //    EndDate = this.coursesService.GetEndDateById(id),
            //    SubjectId = this.coursesService.GetSubjectIdById(id),
            //    SubjectsItems = this.subjectsService.GetAllAsSelectListItems(),
            //};

            EditCourseInputModel input = this.coursesService.GetById<EditCourseInputModel>(id);
            input.SubjectsItems = this.subjectsService.GetAllAsSelectListItems();

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.SubjectsItems = this.subjectsService.GetAllAsSelectListItems();
                return this.View(input);
            }

            input.Id = id;
            await this.coursesService.UpdateAsync(input);
            this.TempData["Message"] = "Course updated successfully!";

            return this.Redirect($"/Courses/AllUnapproved");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.coursesService.DeleteAsync(id);
            this.TempData["Message"] = "Course deleted successfully!";
            return this.Redirect("/");
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Approve(int id)
        {
            await this.coursesService.ApproveAsync(id);
            this.TempData["Message"] = "Course approved successfully!";

            return this.Redirect("/");
        }
    }
}
