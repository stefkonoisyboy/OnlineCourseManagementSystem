namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class FileController : Controller
    {
        private readonly IFileService fileService;
        private readonly UserManager<ApplicationUser> userManager;

        public FileController(IFileService fileService, UserManager<ApplicationUser> userManager)
        {
            this.fileService = fileService;
            this.userManager = userManager;
        }

        public IActionResult AddImageToGallery()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddImageToGallery(UploadImageInputModel inputModel)
        {
            ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);
            inputModel.UserId = applicationUser.Id;
            await this.fileService.UploadImage(inputModel);
            return this.RedirectToAction("AllImages", "File");
        }

        public async Task<IActionResult> AllImages()
        {
            ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);

            IEnumerable<ImageViewModel> images = this.fileService.GetAllImagesForUser(applicationUser.Id);
            return this.View(images);
        }

        public async Task<ActionResult> DeleteImage(int id)
        {
            ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);

            await this.fileService.DeleteImageFromGallery(id, applicationUser.Id);

            return this.RedirectToAction("AllImages", "File");
        }
    }
}
