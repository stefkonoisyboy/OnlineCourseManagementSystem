namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;

    public class LecturesController : Controller
    {
        private readonly ILecturesService lecturesService;
        private readonly UserManager<ApplicationUser> userManager;

        public LecturesController(ILecturesService lecturesService, UserManager<ApplicationUser> userManager)
        {
            this.lecturesService = lecturesService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLectureInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.lecturesService.CreateAsync(input);
            this.TempData["Message"] = "Lecture created successfully!";

            return this.Redirect("/");
        }
    }
}
