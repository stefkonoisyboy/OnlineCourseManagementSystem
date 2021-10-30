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
    using OnlineCourseManagementSystem.Web.ViewModels.Choices;
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class QuestionsController : Controller
    {
        private readonly IQuestionsService questionsService;
        private readonly IChoicesService choicesService;
        private readonly IExamsService examsService;

        public QuestionsController(IQuestionsService questionsService, IChoicesService choicesService, IExamsService examsService)
        {
            this.questionsService = questionsService;
            this.choicesService = choicesService;
            this.examsService = examsService;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("Add Question", FromAction = "All", FromController = typeof(ExamsController))]
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

            try
            {
                input.ExamId = examId;
                await this.questionsService.CreateAsync(input);
                this.TempData["Message"] = "Question successfully created!";
            }
            catch (Exception ex)
            {
                this.TempData["Alert"] = ex.Message;
            }

            return this.RedirectToAction(nameof(this.AllByExam), new { examId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Edit(int id)
        {
            int examId = this.examsService.GetExamIdByQuestionId(id);
            EditQuestionInputModel input = this.questionsService.GetById<EditQuestionInputModel>(id);
            input.Choices = this.choicesService.GetAllById<CreateChoiceInputModel>(id).ToList();

            BreadcrumbNode allExamsNode = new MvcBreadcrumbNode("All", "Exams", "All Exams");

            BreadcrumbNode viewQuestionsNode = new MvcBreadcrumbNode("AllByExam", "Questions", "View Questions")
            {
                Parent = allExamsNode,
                RouteValues = new { examId = examId },
            };

            BreadcrumbNode editQuestionNode = new MvcBreadcrumbNode("Edit", "Questions", "Edit Question")
            {
                Parent = viewQuestionsNode,
            };

            this.ViewData["BreadcrumbNode"] = editQuestionNode;

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

            try
            {
                await this.questionsService.UpdateAsync(input);
                this.TempData["Message"] = "Question successfully updated!";
            }
            catch (Exception ex)
            {
                this.TempData["Alert"] = ex.Message;
            }

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
        [Breadcrumb("View Questions", FromAction = "All", FromController = typeof(ExamsController))]
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
            int examId = this.examsService.GetExamIdByQuestionId(id);
            QuestionDetailsViewModel viewModel = this.questionsService.GetById<QuestionDetailsViewModel>(id);

            BreadcrumbNode allExamsNode = new MvcBreadcrumbNode("All", "Exams", "All Exams");

            BreadcrumbNode viewQuestionsNode = new MvcBreadcrumbNode("AllByExam", "Questions", "View Questions")
            {
                Parent = allExamsNode,
                RouteValues = new { examId = examId },
            };

            BreadcrumbNode detailsQuestionNode = new MvcBreadcrumbNode("Edit", "Questions", "Edit Question")
            {
                Parent = viewQuestionsNode,
            };

            this.ViewData["BreadcrumbNode"] = detailsQuestionNode;

            return this.View(viewModel);
        }
    }
}
