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
    using OnlineCourseManagementSystem.Web.ViewModels.Orders;
    using SmartBreadcrumbs.Attributes;
    using Stripe;

    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrdersService ordersService, ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            this.ordersService = ordersService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> Create(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (this.coursesService.IsCourseAvailable(id, user.Id))
            {
                if (this.ordersService.IsOrderAvailable(id, user.Id))
                {
                    await this.ordersService.CreateAsync(id, user.Id);
                    this.TempData["Message"] = "Course added successfully to cart!";
                }
                else
                {
                    this.TempData["AlertMessage"] = "Order with such course and client already exists in your cart!";
                }
            }
            else
            {
                this.TempData["AlertMessage"] = "You have already purchased this course!";
            }

            return this.RedirectToAction(nameof(this.AllByUserId));
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.ordersService.DeleteAsync(id, user.Id);
            this.TempData["Message"] = "Course removed successfully from cart!";
            return this.RedirectToAction(nameof(this.AllByUserId), new { user.Id });
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb(" My Cart ", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> AllByUserId()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            AllOrdersByUserIdListViewModel viewModel = new AllOrdersByUserIdListViewModel
            {
                Orders = this.ordersService.GetAllByUserId<AllOrdersByUserIdViewModel>(user.Id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Charge(string stripeEmail, string stripeToken)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllOrdersByUserIdListViewModel viewModel = new AllOrdersByUserIdListViewModel
            {
                Orders = this.ordersService.GetAllByUserId<AllOrdersByUserIdViewModel>(user.Id),
            };

            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken,
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = (int)viewModel.Orders.Sum(o => o.CoursePrice) * 100,
                Description = "Sample Charge",
                Currency = "usd",
                Customer = customer.Id,
            });

            foreach (var course in viewModel.Orders.Select(o => o.CourseId))
            {
                await this.coursesService.EnrollAsync(course, user.Id, System.IO.Path.Combine("Datasets", "userInCourses.csv"));
            }

            foreach (var course in viewModel.Orders.Select(o => o.CourseId))
            {
                await this.ordersService.DeleteAsync(course, user.Id);
            }

            return this.Redirect($"/Courses/AllByCurrentUser");
        }
    }
}
