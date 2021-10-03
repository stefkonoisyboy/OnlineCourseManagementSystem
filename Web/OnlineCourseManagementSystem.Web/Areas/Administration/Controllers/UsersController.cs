namespace OnlineCourseManagementSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class UsersController : AdministrationController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult All()
        {
            AllUsersListViewModel viewModel = new AllUsersListViewModel
            {
                Users = this.usersService.GetAll<AllUsersViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
