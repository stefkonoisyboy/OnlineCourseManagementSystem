namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;

    public class QuestionsController : Controller
    {
        private readonly IQuestionsService questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            this.questionsService = questionsService;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create(int examId)
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(int examId, CreateQuestionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            input.ExamId = examId;
            await this.questionsService.CreateAsync(input);
            this.TempData["Message"] = "Question successfully created!";

            return this.RedirectToAction(nameof(this.AllByExam), new { examId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Edit(int id)
        {
            EditQuestionInputModel input = this.questionsService.GetById<EditQuestionInputModel>(id);
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditQuestionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.questionsService.UpdateAsync(input);
            this.TempData["Message"] = "Question successfully updated!";

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int examId)
        {
            await this.questionsService.DeleteAsync(id);
            this.TempData["Message"] = "Question successfully deleted!";
            return this.RedirectToAction(nameof(this.AllByExam), new { examId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AllByExam(int examId)
        {
            AllQuestionsByExamListViewModel viewModel = new AllQuestionsByExamListViewModel
            {
                Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(examId),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Details(int id)
        {
            QuestionDetailsViewModel viewModel = this.questionsService.GetById<QuestionDetailsViewModel>(id);

            return this.View(viewModel);
        }
    }
}
