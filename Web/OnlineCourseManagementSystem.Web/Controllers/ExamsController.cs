namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Answers;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;

    public class ExamsController : Controller
    {
        private readonly IExamsService examsService;
        private readonly ICoursesService coursesService;
        private readonly ILecturersService lecturersService;
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExamsController(
            IExamsService examsService,
            ICoursesService coursesService,
            ILecturersService lecturersService,
            IQuestionsService questionsService,
            IAnswersService answersService,
            UserManager<ApplicationUser> userManager)
        {
            this.examsService = examsService;
            this.coursesService = coursesService;
            this.lecturersService = lecturersService;
            this.questionsService = questionsService;
            this.answersService = answersService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create()
        {
            CreateExamInputModel input = new CreateExamInputModel
            {
                CourseItems = this.coursesService.GetAllAsSelectListItems(),
            };

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateExamInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseItems = this.coursesService.GetAllAsSelectListItems();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.LecturerId = user.Id;
            await this.examsService.CreateAsync(input);
            this.TempData["Message"] = "Exam successfully created!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Edit(int id)
        {
            EditExamInputModel input = this.examsService.GetById<EditExamInputModel>(id);
            input.CourseItems = this.coursesService.GetAllAsSelectListItems();

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExamInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseItems = this.coursesService.GetAllAsSelectListItems();
                return this.View(input);
            }

            await this.examsService.UpdateAsync(input);
            this.TempData["Message"] = "Exam successfully updated!";

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.examsService.DeleteAsync(id);
            this.TempData["Message"] = "Exam successfully deleted!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult All()
        {
            AllExamsListViewModel viewModel = new AllExamsListViewModel
            {
                Exams = this.examsService.GetAll<AllExamsViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> AllByUser()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllExamsByUserIdListViewModel viewModel = new AllExamsByUserIdListViewModel
            {
                Exams = this.examsService.GetAllByUserId<AllExamsByUserIdViewModel>(user.Id),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> MyResults()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            IEnumerable<ResultFromExamViewModel> viewModel = this.examsService.GetAllByCurrentUserId<ResultFromExamViewModel>(user.Id);

            foreach (var item in viewModel)
            {
                item.Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(item.ExamId);
                item.Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(item.ExamId, user.Id);
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Details(int id)
        {
            ExamDetailsViewModel viewModel = this.examsService.GetById<ExamDetailsViewModel>(id);

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> TakeExam(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            TakeExamInputModel input = new TakeExamInputModel
            {
                ExamId = id,
                Duration = this.examsService.GetDurationById(id),
                Name = this.examsService.GetNameById(id),
                Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(id),
                Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(id, user.Id),
            };

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [HttpPost]
        public async Task<IActionResult> TakeExam(int id, IFormCollection formCollection)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.examsService.TakeExamAsync(id, user.Id, formCollection);
            ResultFromExamViewModel viewModel = this.examsService.GetByExamIdAndUserId<ResultFromExamViewModel>(user.Id, id);
            viewModel.Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(id);
            viewModel.Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(id, user.Id);
            int usersCountOnCertainExam = this.examsService.GetCountOfAllUsersWhoPassedCertainExam(id);
            double usersCountWithLowerGradesOnCertainExam = this.examsService.GetCountOfUsersWithLowerGradesOnCertainExam(id, viewModel.Grade);
            viewModel.CompareRateInPercents = ((double)(usersCountWithLowerGradesOnCertainExam / usersCountOnCertainExam)) * 100;

            return this.View("Result", viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> Review(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            ResultFromExamViewModel viewModel = this.examsService.GetByExamIdAndUserId<ResultFromExamViewModel>(user.Id, id);
            viewModel.Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(id);
            viewModel.Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(id, user.Id);
            int usersCountOnCertainExam = this.examsService.GetCountOfAllUsersWhoPassedCertainExam(id);
            double usersCountWithLowerGradesOnCertainExam = this.examsService.GetCountOfUsersWithLowerGradesOnCertainExam(id, viewModel.Grade);
            viewModel.CompareRateInPercents = ((double)(usersCountWithLowerGradesOnCertainExam / usersCountOnCertainExam)) * 100;

            return this.View("Result", viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [HttpPost]
        public async Task<IActionResult> SaveAnswer(int id, int page, IFormCollection formCollection)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.examsService.SaveAnswerAsync(user.Id, formCollection);
            return this.RedirectToAction(nameof(this.TakeExam), new { id, page });
        }
    }
}
