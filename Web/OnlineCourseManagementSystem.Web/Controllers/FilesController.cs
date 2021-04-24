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

    public class FilesController : Controller
    {
        private readonly IFilesService fileService;
        private readonly UserManager<ApplicationUser> userManager;

        public FilesController(IFilesService fileService, UserManager<ApplicationUser> userManager)
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
        public async Task<IActionResult> AddImageToGallery(UploadImageInputModel inputModel, int id)
        {
            ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);
            inputModel.UserId = applicationUser.Id;
            inputModel.AlbumId = id;
            await this.fileService.AddImages(inputModel);
            return this.RedirectToAction("AllImages", "Files", new { Id = id });
        }

        public async Task<IActionResult> AllImages(int id)
        {
            ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);

            var images = this.fileService.GetAllImagesForUser(applicationUser.Id, id);
            return this.View(images);
        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);

            int albumId = (int)await this.fileService.DeleteImageFromGallery(id, applicationUser.Id);

            return this.RedirectToAction("AllImages", "Files", new { Id = albumId });
        }
    }
}
