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
    using OnlineCourseManagementSystem.Web.ViewModels.Albums;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumService;
        private readonly UserManager<ApplicationUser> userManager;

        public AlbumsController(IAlbumsService albumService, UserManager<ApplicationUser> userManager)
        {
            this.albumService = albumService;
            this.userManager = userManager;
        }

        [Breadcrumb("My Albums", FromAction = "Index", FromController =typeof(HomeController))]
        public async Task<IActionResult> All()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllAlbumsViewModel allAlbumsViewModel = new AllAlbumsViewModel
            {
                Albums = this.albumService.GetAllById<AlbumViewModel>(user.Id),
            };

            return this.View(allAlbumsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(AlbumInputModel inputModel)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            inputModel.UserId = user.Id;
            try
            {
                await this.albumService.CreateAsync(inputModel);
            }
            catch (Exception e)
            {
                this.TempData["AlbumExist"] = e.Message;
            }

            return this.RedirectToAction("All", "Albums");
        }
    }
}
