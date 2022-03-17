namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClosedXML.Excel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
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
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsController(
            IQuestionsService questionsService,
            IChoicesService choicesService,
            IExamsService examsService,
            UserManager<ApplicationUser> userManager)
        {
            this.questionsService = questionsService;
            this.choicesService = choicesService;
            this.examsService = examsService;
            this.userManager = userManager;
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
        public IActionResult AllByExam(int examId, string input, int id = 1)
        {
            const int ItemsPerPage = 5;

            AllQuestionsByExamListViewModel viewModel = new AllQuestionsByExamListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ActiveCoursesCount = this.questionsService.GetCountByExamId(examId, input),
                Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(examId, id, input, ItemsPerPage),
                Name = input,
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

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> ExportAllExamsAsCSV(int examId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Exam,Question");

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllQuestionsByExamListViewModel viewModel = new AllQuestionsByExamListViewModel
            {
                Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(examId, 1, string.Empty, int.MaxValue),
            };

            foreach (var question in viewModel.Questions)
            {
                sb.AppendLine($"{question.ExamName},{question.SanitizedText}");
            }

            return this.File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "QuestionsInfo.csv");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> ExportAllExamsAsExcel(int examId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllQuestionsByExamListViewModel viewModel = new AllQuestionsByExamListViewModel
            {
                Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(examId, 1, string.Empty, int.MaxValue),
            };

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MyExams");
                int currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Exam";
                worksheet.Cell(currentRow, 2).Value = "Question";

                foreach (var question in viewModel.Questions)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = question.ExamName;
                    worksheet.Cell(currentRow, 2).Value = question.SanitizedText;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return this.File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "QuestionsInfo.xlsx");
                }
            }
        }
    }
}
