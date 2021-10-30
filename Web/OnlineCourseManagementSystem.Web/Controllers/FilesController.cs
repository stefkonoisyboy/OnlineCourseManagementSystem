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
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using SmartBreadcrumbs.Nodes;

    public class FilesController : Controller
    {
        private readonly IFilesService fileService;
        private readonly ICoursesService coursesService;
        private readonly ILecturesService lecturesService;
        private readonly UserManager<ApplicationUser> userManager;

        public FilesController(
            IFilesService fileService,
            ICoursesService coursesService,
            ILecturesService lecturesService,
            UserManager<ApplicationUser> userManager)
        {
            this.fileService = fileService;
            this.coursesService = coursesService;
            this.lecturesService = lecturesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Lecturer,Student")]
        public IActionResult ById(int id, int lectureId)
        {
            int courseId = this.lecturesService.GetCourseIdByLectureId(lectureId);
            FileByIdViewModel viewModel = this.fileService.GetById<FileByIdViewModel>(id);
            viewModel.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
            viewModel.Files = this.fileService.GetAllById<AllFilesByLectureIdViewModel>(lectureId, id);

            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentUser", "Courses", "My Courses");

            BreadcrumbNode courseDetailsNode = new MvcBreadcrumbNode("ById", "Courses", "Course Details")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = courseId },
            };
            BreadcrumbNode fileDetailsNode = new MvcBreadcrumbNode("ById", "Files", "Resource Details")
            {
                Parent = courseDetailsNode,
                RouteValues = new { id = id, lectureId = lectureId },
            };

            this.ViewData["BreadcrumbNode"] = fileDetailsNode;

            return this.View(viewModel);
        }

        [Authorize(Roles = "Lecturer,Student")]
        public IActionResult VideoById(int id, int courseId)
        {
            VideoByIdViewModel viewModel = this.fileService.GetById<VideoByIdViewModel>(id);
            viewModel.Lectures = this.lecturesService.GetAllById<AllLecturesByIdViewModel>(courseId);

            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentUser", "Courses", "My Courses");

            BreadcrumbNode courseDetailsNode = new MvcBreadcrumbNode("ById", "Courses", "Course Details")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = courseId },
            };
            BreadcrumbNode videoDetailsNode = new MvcBreadcrumbNode("VideoById", "Files", "Video Details")
            {
                Parent = courseDetailsNode,
                RouteValues = new { id = id, courseId = courseId },
            };

            this.ViewData["BreadcrumbNode"] = videoDetailsNode;

            return this.View(viewModel);
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

        public async Task<IActionResult> DeleteWorkFileFromAssignment(int id)
        {
            int assignmentId = (int)await this.fileService.DeleteWorkFileFromAssignment(id);

            return this.RedirectToAction("GetInfo", "Assignments", new { Id = assignmentId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            int? lectureId = await this.fileService.DeleteAsync(id);
            int courseId = this.lecturesService.GetCourseIdByLectureId(lectureId.Value);
            this.TempData["Message"] = "File successfully deleted!";
            return this.Redirect($"/Courses/ById/{courseId}");
        }
    }
}
