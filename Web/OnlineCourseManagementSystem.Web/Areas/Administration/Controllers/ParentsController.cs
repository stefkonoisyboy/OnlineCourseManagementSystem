namespace OnlineCourseManagementSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Parents;

    public class ParentsController : AdministrationController
    {
        private readonly IParentsService parentsService;

        public ParentsController(IParentsService parentsService)
        {
            this.parentsService = parentsService;
        }

        public IActionResult All()
        {
            AllParentsListViewModel viewModel = new AllParentsListViewModel
            {
                Parents = this.parentsService.GetAll<AllParentsViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
