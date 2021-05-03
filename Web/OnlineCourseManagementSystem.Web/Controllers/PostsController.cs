namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Comments;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Posts;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ICoursesService coursesService;
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly int ItemsPerPage = 2;

        public PostsController(IPostsService postsService, ICoursesService coursesService, ICommentsService commentsService ,UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.coursesService = coursesService;
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            CreatePostInputModel inputModel = new CreatePostInputModel
            {
                CourseItems = this.coursesService.GetAllAsSelectListItems(),
            };

            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CourseItems = this.coursesService.GetAllAsSelectListItems();
                return this.View(inputModel);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            inputModel.AuthorId = user.Id;

            await this.postsService.CreateAsync(inputModel);
            this.TempData["CreatedPost"] = "Successfully created post";
            return this.RedirectToAction("All", "Posts");
        }

        public IActionResult All(int id, string search, int courseId)
        {
            id = Math.Max(1, id);
            var skip = (id - 1) * this.ItemsPerPage;
            var query = this.postsService.GetAll<PostViewModel>();

            if (search != null)
            {
                query = query.Where(p => p.Title.Contains(search));
            }

            if (courseId != 0)
            {
                query = query.Where(p => p.CourseId == courseId);
            }

            var posts = query
                .Skip(skip)
                .Take(this.ItemsPerPage)
                .ToList();

            foreach (var post in posts)
            {
                post.LastActive = this.commentsService.GetLastActiveCommentByPostId<LastActiveViewModel>(post.Id);
            }

            var postsCount = query.Count();
            var pagesCount = (int)Math.Ceiling(postsCount / (decimal)this.ItemsPerPage);
            AllPostsViewModel viewModel = new AllPostsViewModel
            {
                Posts = posts,
                Courses = this.coursesService.GetAll<CourseViewModel>(),
                CurrentPage = id,
                PagesCount = pagesCount,
                PostsCount = postsCount,
                Search = search,
            };

            return this.View(viewModel);
        }

        public IActionResult SeePost(int id)
        {
            PostInfoViewModel postInfoViewModel = new PostInfoViewModel
            {
                Post = this.postsService.GetById<PostViewModel>(id),
                Comments = this.commentsService.GetAllByPostId<CommentViewModel>(id),
            };

            foreach (var comment in postInfoViewModel.Comments)
            {
                comment.Replies = this.commentsService.GetAllReplies<CommentViewModel>(comment.Id);
            }

            return this.View(postInfoViewModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            EditPostInputModel editPostInputModel = this.postsService.GetById<EditPostInputModel>(id);

            editPostInputModel.CourseItems = this.coursesService.GetAllAsSelectListItems();
            return this.View(editPostInputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditPostInputModel inputModel, int id)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel = this.postsService.GetById<EditPostInputModel>(id);
                inputModel.CourseItems = this.coursesService.GetAllAsSelectListItems();

                return this.View(inputModel);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            inputModel.AuthorId = user.Id;
            inputModel.PostId = id;
            await this.postsService.UpdateAsync(inputModel);

            return this.RedirectToAction("All", "Posts");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.postsService.DeleteAsync(id);

            return this.RedirectToAction("All", "Posts");
        }

        [HttpPost]
        public IActionResult Search(SearchInputModel inputModel)
        {
            AllPostsViewModel viewModel = new AllPostsViewModel
            {
                Posts = this.postsService.SearchByTitle<PostViewModel>(inputModel),
            };

            return this.View(viewModel);
        }
    }
}
