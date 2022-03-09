namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;

    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(IFormCollection form)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string content = form["Content"].ToString();
            int courseId = int.Parse(form["CourseId"]);
            double rating = double.Parse(form["Rating"]);

            CreateReviewInputModel input = new CreateReviewInputModel
            {
                UserId = userId,
                Content = content,
                CourseId = courseId,
                Rating = rating,
            };

            await this.reviewsService.CreateAsync(input);
            this.TempData["Message"] = "Added Review successfully!";

            return this.Redirect($"/Courses/Details/{courseId}");
        }
    }
}
