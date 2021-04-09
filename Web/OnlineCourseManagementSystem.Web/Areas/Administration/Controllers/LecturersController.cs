namespace OnlineCourseManagementSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;

    public class LecturersController : AdministrationController
    {
        private readonly ILecturersService lecturersService;

        public LecturersController(ILecturersService lecturersService)
        {
            this.lecturersService = lecturersService;
        }

        public IActionResult All()
        {
            AllLecturersListViewModel viewModel = new AllLecturersListViewModel
            {
                Lecturers = this.lecturersService.GetAll<AllLecturersViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
