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
    using OnlineCourseManagementSystem.Web.ViewModels.Modules;
    using OnlineCourseManagementSystem.Web.ViewModels.Subjects;
    using OnlineCourseManagementSystem.Web.ViewModels.Trainings;
    using SmartBreadcrumbs.Attributes;

    public class TrainingsController : Controller
    {
        private readonly ITrainingsService trainingsService;
        private readonly IModulesService modulesService;
        private readonly ISubjectsService subjectsService;

        public TrainingsController(ITrainingsService trainingsService, IModulesService modulesService, ISubjectsService subjectsService)
        {
            this.trainingsService = trainingsService;
            this.modulesService = modulesService;
            this.subjectsService = subjectsService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            CreateTrainingInputModel inputModel = new CreateTrainingInputModel()
            {
                ModuleItems = this.modulesService.GetAllAsSelectListItems(),
            };

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateTrainingInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.ModuleItems = this.modulesService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            try
            {
                await this.trainingsService.CreateAsync(inputModel);
            }
            catch (Exception ex)
            {
                this.TempData["ErrorMessage"] = ex.Message;
                return this.View();
            }

            return this.RedirectToAction("Index", "Home");
        }

        [Breadcrumb(FromAction ="Index", FromController = typeof(HomeController))]
        public IActionResult All()
        {
            AllTrainingsViewModel viewModel = new AllTrainingsViewModel()
            {
                Trainings = this.trainingsService.GetAll<TrainingViewModel>(),
                Module = this.modulesService.GetById<ModuleViewModel>(15),
                Fundamentals = this.modulesService.GetById<ModuleViewModel>(16),
            };

            foreach (var training in viewModel.Trainings)
            {
                foreach (var module in training.Modules)
                {
                    module.Subjects = this.subjectsService.GetAllByModule<SubjectViewModel>(module.Id);
                }
            }

            return this.View(viewModel);
        }
    }
}
