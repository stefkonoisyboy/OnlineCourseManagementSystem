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
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Dashboard;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;

    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(IDashboardService dashboardService, IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.dashboardService = dashboardService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb("Dashboard", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> StudentDashboard()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            DashboardViewModel viewModel = new DashboardViewModel
            {
                AverageSuccess = this.dashboardService.GetAverageSuccessByUserId(user.Id),
                Assignments = this.dashboardService.GetFinishedAssignmentsCountByUserId(user.Id),
                RankingForAverageSuccess = this.dashboardService.GetRankingForAverageSuccess(user.Id),
                RankingForFinishedAssignments = this.dashboardService.GetRankingForFinishedAssignments(user.Id),
                Channels = this.dashboardService.GetChannelsCountByUserId(user.Id),
                ExamsChart = this.dashboardService.GetTop3CoursesByUserId<CoursesChartViewModel>(user.Id),
                CoursesEnrolled = this.dashboardService.GetCoursesEnrolledCountByUserId(user.Id),
                Events = this.dashboardService.GetEventsCount(),
                Exams = this.dashboardService.GetCompletedExamsCountByUserId(user.Id),
                RankingForEnrolledCourses = this.dashboardService.GetRankingForEnrolledCourses(user.Id),
                User = this.dashboardService.GetUserInfoById<DashboardUserViewModel>(user.Id),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb("All Stats", FromAction = "StudentDashboard")]
        public async Task<IActionResult> StudentDashboardAllStats()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            DashboardViewModel viewModel = new DashboardViewModel
            {
                AverageSuccess = this.dashboardService.GetAverageSuccessByUserId(user.Id),
                Assignments = this.dashboardService.GetFinishedAssignmentsCountByUserId(user.Id),
                RankingForAverageSuccess = this.dashboardService.GetRankingForAverageSuccess(user.Id),
                RankingForFinishedAssignments = this.dashboardService.GetRankingForFinishedAssignments(user.Id),
                Channels = this.dashboardService.GetChannelsCountByUserId(user.Id),
                ExamsChart = this.dashboardService.GetTop3CoursesByUserId<CoursesChartViewModel>(user.Id),
                CoursesEnrolled = this.dashboardService.GetCoursesEnrolledCountByUserId(user.Id),
                Events = this.dashboardService.GetEventsCount(),
                Exams = this.dashboardService.GetCompletedExamsCountByUserId(user.Id),
                RankingForEnrolledCourses = this.dashboardService.GetRankingForEnrolledCourses(user.Id),
                User = this.dashboardService.GetUserInfoById<DashboardUserViewModel>(user.Id),
                CompletedLectures = this.dashboardService.GetTop3CoursesWithMostCompletedLecturesByUserId<CompletedLecturesByCourseViewModel>(user.Id),
                CompletedAssignments = this.dashboardService.GetTop3CoursesWithMostCompletedAssignmentsByUserId<CompletedAssignmentsByCourseViewModel>(user.Id),
            };

            foreach (var completedLecture in viewModel.CompletedLectures)
            {
                completedLecture.CompletedLectures = this.dashboardService.GetCompletedLecturesByCourseIdAndUserId(completedLecture.Id, user.Id);
            }

            foreach (var completedAssignment in viewModel.CompletedAssignments)
            {
                completedAssignment.CompletedAssignments = this.dashboardService.GetCompletedAssignmentsByCourseIdAndUserId(completedAssignment.Id, user.Id);
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("Dashboard", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> LecturerDashboard()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            DashboardViewModel viewModel = new DashboardViewModel
            {
                CourseCreated = this.dashboardService.GetCreatedCoursesCountByCreatorId(user.Id),
                LecturesCreated = this.dashboardService.GetCreatedLecturesCountByCreatorId(user.Id),
                ExamsCreated = this.dashboardService.GetCreatedExamsCountByCreatorId(user.Id),
                AssignmentsCreated = this.dashboardService.GetCreatedAssignmentsCountByCreatorId(user.Id),
                ChannelsCreated = this.dashboardService.GetCreatedChannelsCountByCreatorId(user.Id),
                Events = this.dashboardService.GetEventsCount(),
                User = this.dashboardService.GetUserInfoById<DashboardUserViewModel>(user.Id),
                RankingForCreatedCourses = this.dashboardService.GetRankingForCreatedCourses(user.Id),
                RankingForCreatedLectures = this.dashboardService.GetRankingForCreatedLectures(user.Id),
                RankingForCreatedAssignments = this.dashboardService.GetRankingForCreatedAssignments(user.Id),
                TopExamsByCreator = this.dashboardService.GetTop3ExamsByCreatorId<Top3ExamsByCreatorIdViewModel>(user.Id),
                TopStudentsByCompletedLectures = this.dashboardService.GetTop3StudentsByCompletedLectures<Top3StudentsByCompletedLecturesViewModel>(),
                TopStudentsByCompletedAssignments = this.dashboardService.GetTop3StudentsByCompletedAssignments<Top3StudentsByCompletedAssignmentsViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("Dashboard", FromAction = "LecturerDashboard")]
        public async Task<IActionResult> LecturerDashboardAllStats()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            DashboardViewModel viewModel = new DashboardViewModel
            {
                CourseCreated = this.dashboardService.GetCreatedCoursesCountByCreatorId(user.Id),
                LecturesCreated = this.dashboardService.GetCreatedLecturesCountByCreatorId(user.Id),
                ExamsCreated = this.dashboardService.GetCreatedExamsCountByCreatorId(user.Id),
                AssignmentsCreated = this.dashboardService.GetCreatedAssignmentsCountByCreatorId(user.Id),
                ChannelsCreated = this.dashboardService.GetCreatedChannelsCountByCreatorId(user.Id),
                Events = this.dashboardService.GetEventsCount(),
                User = this.dashboardService.GetUserInfoById<DashboardUserViewModel>(user.Id),
                RankingForCreatedCourses = this.dashboardService.GetRankingForCreatedCourses(user.Id),
                RankingForCreatedLectures = this.dashboardService.GetRankingForCreatedLectures(user.Id),
                RankingForCreatedAssignments = this.dashboardService.GetRankingForCreatedAssignments(user.Id),
                TopExamsByCreator = this.dashboardService.GetTop3ExamsByCreatorId<Top3ExamsByCreatorIdViewModel>(user.Id),
                TopStudentsByCompletedLectures = this.dashboardService.GetTop3StudentsByCompletedLectures<Top3StudentsByCompletedLecturesViewModel>(),
                TopStudentsByCompletedAssignments = this.dashboardService.GetTop3StudentsByCompletedAssignments<Top3StudentsByCompletedAssignmentsViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Breadcrumb("Dashboard", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult AdminDashboard()
        {
            AdminDashboardViewModel viewModel = new AdminDashboardViewModel
            {
                TotalCourses = this.dashboardService.GetAllCoursesCount(),
                TotalStudents = this.dashboardService.GetAllStudentsCount(),
                TotalTeachers = this.dashboardService.GetAllTeachersCount(),
                Teachers = this.usersService.GetTop4Teachers<Top4TeachersViewModel>(),
                Students = this.usersService.GetTop4Students<Top4StudentsViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
