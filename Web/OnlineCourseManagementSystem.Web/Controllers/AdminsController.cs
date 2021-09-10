namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Admins;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Events;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Posts;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class AdminsController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly IEventsService eventsService;
        private readonly ILecturesService lecturesService;
        private readonly IExamsService examsService;
        private readonly IPostsService postsService;
        private readonly IUsersService usersService;
        private readonly IAssignmentsService assignmentsService;

        public AdminsController(
            ICoursesService coursesService,
            IEventsService eventsService,
            ILecturesService lecturesService,
            IExamsService examsService,
            IPostsService postsService,
            IUsersService usersService,
            IAssignmentsService assignmentsService)
        {
            this.coursesService = coursesService;
            this.eventsService = eventsService;
            this.lecturesService = lecturesService;
            this.examsService = examsService;
            this.postsService = postsService;
            this.usersService = usersService;
            this.assignmentsService = assignmentsService;
        }

        public IActionResult AdminActions()
        {
            AdminItemsViewModel viewModel = new AdminItemsViewModel()
            {
                Courses = this.coursesService.GetAllByAdmin<CourseByAdminViewModel>(),
                Events = this.eventsService.GetAllByAdmin<EventByAdminViewModel>(),
                Lectures = this.lecturesService.GetAllByAdmin<LectureByAdminViewModel>(),
                Exams = this.examsService.GetAllByAdmin<ExamByAdminViewModel>(),
                Posts = this.postsService.GetAllByAdmin<PostByAdminViewModel>(),
                Assignments = this.assignmentsService.GetAllByAdmin<AssignmentByAdminViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
