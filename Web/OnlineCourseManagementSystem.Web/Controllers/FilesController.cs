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
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Events;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using SmartBreadcrumbs.Nodes;

    public class FilesController : Controller
    {
        private readonly IFilesService fileService;
        private readonly ICoursesService coursesService;
        private readonly ILecturesService lecturesService;
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;

        public FilesController(
            IFilesService fileService,
            ICoursesService coursesService,
            ILecturesService lecturesService,
            IEventsService eventsService,
            UserManager<ApplicationUser> userManager)
        {
            this.fileService = fileService;
            this.coursesService = coursesService;
            this.lecturesService = lecturesService;
            this.eventsService = eventsService;
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
            viewModel.Lectures = this.lecturesService.GetAllById<AllLecturesByIdViewModel>(courseId, 1, 100000);

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

        [Authorize]
        public async Task<IActionResult> MarkAsCompleted(int id, int courseId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            await this.fileService.MarkAsCompletedAsync(id, user.Id);
            this.TempData["Message"] = "File marked as completed successfully!";

            return this.RedirectToAction("ById", "Courses", new { id = courseId });
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

            BreadcrumbNode myalbumsNode = new MvcBreadcrumbNode("All", "Albums", "My Albums");

            BreadcrumbNode myimagesNode = new MvcBreadcrumbNode("AllImages", "Files", "Images By Album")
            {
                Parent = myalbumsNode,
                RouteValues = new { id = id, },
            };

            this.ViewData["BreadcrumbNode"] = myimagesNode;
            var images = this.fileService.GetAllImagesForUserByAlbum<AllImagesViewModel>(applicationUser.Id, id);
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

        public async Task<IActionResult> DeleteFileFromEventByAdmin(int id)
        {
            int? eventId = await this.fileService.DeleteFromEventAsync(id);
            this.TempData["Message"] = "File successfully deleted!";
            return this.RedirectToAction("EditEvent", "Admins", new { id = eventId });
        }

        [HttpPost]
        [Authorize(Roles ="Lecturer,Administrator")]
        public async Task<IActionResult> AddVideoToEvent(VideoFileInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            int videoId = await this.fileService.AddVideoResourceToEventAsync(inputModel);
            this.TempData["Message"] = "Successfully added video resource!";
            return this.RedirectToAction("EventVideoById", "Files", new { id = videoId });
        }

        public IActionResult EventVideoById(int id)
        {
            EventVideoByIdViewModel viewModel = this.fileService.GetById<EventVideoByIdViewModel>(id);
            viewModel.Events = this.eventsService.GetAll<EventViewModel>();
            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteFileFromEvent(int id)
        {
            int? eventId = await this.fileService.DeleteFromEventAsync(id);
            this.TempData["Message"] = "File successfully deleted!";
            return this.RedirectToAction("Edit", "Events", new { id = eventId });
        }
    }
}
