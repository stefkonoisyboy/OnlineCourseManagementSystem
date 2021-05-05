using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Data;
using OnlineCourseManagementSystem.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentInputModel inputModel, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(id);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            inputModel.AuthorId = user.Id;
            inputModel.PostId = id;

            await this.commentsService.CreateAsync(inputModel);

            this.TempData["CreatedComment"] = "Successfully created a comment.";

            return this.RedirectToAction("SeePost", "Posts", new { Id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            int? postId = await this.commentsService.DeleteAsync(id);
            return this.RedirectToAction("SeePost", "Posts", new { Id = postId });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            EditCommentInputModel inputModel = this.commentsService.GetById<EditCommentInputModel>(id);
            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentInputModel inputModel, int id)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel = this.commentsService.GetById<EditCommentInputModel>(id);
                return this.View(inputModel);
            }

            int? postId = this.commentsService.GetPostId(id);
            inputModel.CommentId = id;
            await this.commentsService.UpdateAsync(inputModel);

            return this.RedirectToAction("SeePost", "Posts", new { Id = postId });
        }

        [Authorize]
        public IActionResult Reply()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Reply(ReplyToCommentInputModel inputModel, int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            int? postId = this.commentsService.GetPostId(id);

            inputModel.PostId = postId;
            inputModel.ParentId = id;
            inputModel.AuthorId = user.Id;
            await this.commentsService.ReplyToComment(inputModel);

            return this.RedirectToAction("SeePost", "Posts", new { Id = postId });
        }

        [Authorize]
        public async Task<IActionResult> Like(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.commentsService.Like(id, user.Id);
            int? postId = this.commentsService.GetPostId(id);

            return this.RedirectToAction("SeePost", "Posts", new { Id = postId });
        }

        [Authorize]
        public async Task<IActionResult> Dislike(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            await this.commentsService.Dislike(id, user.Id);
            int? postId = this.commentsService.GetPostId(id);

            return this.RedirectToAction("SeePost", "Posts", new { Id = postId });
        }
    }
}
