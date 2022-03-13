namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Modules;

    public class ModulesController : Controller
    {
        private readonly IModulesService modulesService;
        private readonly ISubjectsService subjectsService;
        private readonly ICoursesService coursesService;

        public ModulesController(IModulesService modulesService, ISubjectsService subjectsService, ICoursesService coursesService)
        {
            this.modulesService = modulesService;
            this.subjectsService = subjectsService;
            this.coursesService = coursesService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            CreateModuleInputModel inputModel = new CreateModuleInputModel()
            {
                SubjectItems = this.subjectsService.GetAllAsSelectListItems(),
            };

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateModuleInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.SubjectItems = this.subjectsService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            await this.modulesService.Create(inputModel);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Edit()
        {
            UpdateModuleInputModel inputModel = new UpdateModuleInputModel()
            {
                SubjectItems = this.subjectsService.GetAllAsSelectListItems(),
            };

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateModuleInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.SubjectItems = this.subjectsService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            await this.modulesService.UpdateAsync(inputModel);
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult ById(int moduleId)
        {
            ModuleByIdViewModel viewModel = new ModuleByIdViewModel()
            {
                Courses = this.coursesService.GetAllTopLatestBySubjectInModule<AllCoursesBySubjectViewModel>(moduleId),
            };

            return this.View(viewModel);
        }
    }
}
