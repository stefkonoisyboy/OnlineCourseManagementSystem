using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Data;
using OnlineCourseManagementSystem.Web.ViewModels.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumService;
        private readonly UserManager<ApplicationUser> userManager;

        public AlbumsController(IAlbumsService albumService, UserManager<ApplicationUser> userManager)
        {
            this.albumService = albumService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            var albums = this.albumService.GetAllById<AlbumViewModel>(user.Id);

            return this.View(albums);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(BaseAlbumInputModel inputModel)
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
