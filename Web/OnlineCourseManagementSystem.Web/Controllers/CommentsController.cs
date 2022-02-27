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
    using OnlineCourseManagementSystem.Web.ViewModels.Comments;

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

            this.TempData["Message"] = "Successfully created a comment.";

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
            this.TempData["Message"] = "Successfully updated comment";

            return this.RedirectToAction("SeePost", "Posts", new { Id = postId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Reply(ReplyToCommentInputModel replyInputModel)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            replyInputModel.AuthorId = user.Id;
            await this.commentsService.ReplyToComment(replyInputModel);

            return this.RedirectToAction("SeePost", "Posts", new { id = replyInputModel.PostId });
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

        [Authorize(Roles= GlobalConstants.AdministratorRoleName)]
        public IActionResult AllToxicComments()
        {
            AllCommentsViewModel viewModel = new AllCommentsViewModel()
            {
                Comments = this.commentsService.GetAllCommentsClassified(),
                DeletedToxicCommentIds = this.commentsService.GetAllCommentsClassified().Where(x => x.Score > 0.5).Select(x => x.Comment.Id),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteAllToxicComments()
        {
            this.commentsService.DeleteAllToxicComments();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
