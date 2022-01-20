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
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ICoursesService coursesService;
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly int itemsPerPage = 5;

        public PostsController(IPostsService postsService, ICoursesService coursesService, ICommentsService commentsService, UserManager<ApplicationUser> userManager)
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
            BreadcrumbNode forumNode = new MvcBreadcrumbNode("All", "Posts", "Forum");
            BreadcrumbNode createpostNode = new MvcBreadcrumbNode("Create", "Posts", "Create Post")
            {
                Parent = forumNode,
            };
            this.ViewData["BreadcrumbNode"] = createpostNode;

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
            this.TempData["Message"] = "Successfully created post";
            return this.RedirectToAction("All", "Posts");
        }

        [Breadcrumb("Forum", FromAction ="Index", FromController =typeof(HomeController))]
        public IActionResult All(string search, int courseId, int id = 1)
        {
            id = Math.Max(1, id);
            var skip = (id - 1) * this.itemsPerPage;
            var query = this.postsService.GetAll<PostViewModel>();

            if (!string.IsNullOrEmpty(search))
            {
                query = this.postsService.SearchByTitle<PostViewModel>(search);
            }

            if (courseId != 0)
            {
                query = this.postsService.GetAllByCourseId<PostViewModel>(courseId);
            }

            var posts = query
                .Skip(skip)
                .Take(this.itemsPerPage)
                .ToList();

            foreach (var post in posts)
            {
                post.LastActive = this.commentsService.GetLastActiveCommentByPostId<LastActiveViewModel>(post.Id);
                if (post.LastActive == null)
                {
                    LastActiveViewModel lastActive = new LastActiveViewModel
                    {
                        Name = post.AuthorName,
                    };

                    if (post.CreatedOn > post.ModifiedOn)
                    {
                        lastActive.LastActive = post.CreatedOn;
                    }
                    else
                    {
                        lastActive.LastActive = post.ModifiedOn;
                    }

                    post.LastActive = lastActive;
                }
            }

            var postsCount = query.Count();
            var pagesCount = (int)Math.Ceiling(postsCount / (decimal)this.itemsPerPage);
            AllPostsViewModel viewModel = new AllPostsViewModel
            {
                Posts = posts.OrderByDescending(p => p.LastActive.LastActive),
                Courses = this.coursesService.GetAll<CourseViewModel>(),
                CountOfAllPosts = this.postsService.GetCoutOfAllPosts(),
                CurrentPage = id,
                PagesCount = pagesCount,
                PostsCount = postsCount,
                Search = search,
                CourseId = courseId,
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

            BreadcrumbNode forumNode = new MvcBreadcrumbNode("All", "Posts", "Forum");
            BreadcrumbNode postNode = new MvcBreadcrumbNode("SeePost", "Posts", "Post")
            {
                Parent = forumNode,
            };

            this.ViewData["BreadcrumbNode"] = postNode;

            return this.View(postInfoViewModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            EditPostInputModel editPostInputModel = this.postsService.GetById<EditPostInputModel>(id);
            BreadcrumbNode forumNode = new MvcBreadcrumbNode("All", "Posts", "Forum");
            BreadcrumbNode editpostNode = new MvcBreadcrumbNode("Edit", "Posts", "Edit Post")
            {
                Parent = forumNode,
            };

            this.ViewData["BreadcrumbNode"] = editpostNode;
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
            inputModel.Id = id;
            await this.postsService.UpdateAsync(inputModel);

            return this.RedirectToAction("All", "Posts");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.postsService.DeleteAsync(id);

            return this.RedirectToAction("All", "Posts");
        }

        public async Task<IActionResult> Like(int id, int currentPage, string search, int courseId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            await this.postsService.Like(id, user.Id);

            return this.RedirectToAction("All", "Posts", new { Id = currentPage, Search = search, CourseId = courseId });
        }

        public async Task<IActionResult> Dislike(int id, int currentPage, string search, int courseId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            await this.postsService.Dislike(id, user.Id);

            return this.RedirectToAction("All", "Posts", new { Id = currentPage, Search = search, CourseId = courseId });
        }
    }
}
