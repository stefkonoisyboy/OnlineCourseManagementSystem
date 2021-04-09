namespace OnlineCourseManagementSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Students;

    public class StudentsController : AdministrationController
    {
        private readonly IStudentsService studentsService;
        private readonly IParentsService parentsService;

        public StudentsController(IStudentsService studentsService, IParentsService parentsService)
        {
            this.studentsService = studentsService;
            this.parentsService = parentsService;
        }

        public IActionResult All()
        {
            AllStudentsListViewModel viewModel = new AllStudentsListViewModel
            {
                Students = this.studentsService.GetAll<AllStudentsViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult AddParent()
        {
            AddParentInputModel input = new AddParentInputModel
            {
                Students = this.studentsService.GetAllAsSelectListItems(),
                Parents = this.parentsService.GetAllAsSelectListItems(),
            };

            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> AddParent(AddParentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Students = this.studentsService.GetAllAsSelectListItems();
                input.Parents = this.parentsService.GetAllAsSelectListItems();
                return this.View(input);
            }

            await this.studentsService.AddParent(input);
            this.TempData["Message"] = "Successfully added/edited parent for student!";

            return this.Redirect("/");
        }
    }
}
