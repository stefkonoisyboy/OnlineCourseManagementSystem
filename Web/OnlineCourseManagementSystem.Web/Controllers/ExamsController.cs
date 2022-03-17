namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClosedXML.Excel;
    using iTextSharp.text.pdf;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Answers;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;
    using OnlineCourseManagementSystem.Web.ViewModels.Subjects;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;

    public class ExamsController : Controller
    {
        private readonly IExamsService examsService;
        private readonly ICoursesService coursesService;
        private readonly ILecturersService lecturersService;
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;
        private readonly IUsersService usersService;
        private readonly ILecturesService lecturesService;
        private readonly ISubjectsService subjectsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExamsController(
            IExamsService examsService,
            ICoursesService coursesService,
            ILecturersService lecturersService,
            IQuestionsService questionsService,
            IAnswersService answersService,
            IUsersService usersService,
            ILecturesService lecturesService,
            ISubjectsService subjectsService,
            UserManager<ApplicationUser> userManager)
        {
            this.examsService = examsService;
            this.coursesService = coursesService;
            this.lecturersService = lecturersService;
            this.questionsService = questionsService;
            this.answersService = answersService;
            this.usersService = usersService;
            this.lecturesService = lecturesService;
            this.subjectsService = subjectsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("Create Exam", FromAction = "All")]
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            CreateExamInputModel input = new CreateExamInputModel
            {
                CourseItems = this.coursesService.GetAllAsSelectListItems(),
            };

            input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
            input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateExamInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CourseItems = this.coursesService.GetAllAsSelectListItems();
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                return this.View(input);
            }

            input.CreatorId = user.Id;
            input.LecturerId = user.Id;
            await this.examsService.CreateAsync(input);
            this.TempData["Message"] = "Exam successfully created!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> AddToLecture(int lectureId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            AddExamToLectureInputModel input = new AddExamToLectureInputModel
            {
                Exams = this.examsService.GetAllExamsAsSelectListItemsByCreatorId(user.Id),
                RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddToLecture(int lectureId, AddExamToLectureInputModel input)
        {
            int courseId = this.lecturesService.GetCourseIdByLectureId(lectureId);
            string lectureName = this.lecturesService.GetNameById(lectureId);

            if (this.examsService.IsExamAddedToLecture(input.ExamId, lectureId))
            {
                this.TempData["Alert"] = $"Exam has been already added to Lecture: {lectureName}";
            }
            else
            {
                try
                {
                    await this.examsService.AddExamToLectureAsync(lectureId, input);
                }
                catch (Exception ex)
                {
                    this.TempData["Alert"] = ex.Message;
                }

                this.TempData["Message"] = $"Exam added successfully to Lecture: {lectureName}";
            }

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.RedirectToAction("ById", "Courses", new { id = courseId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("Edit Exam", FromAction = "All")]
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            EditExamInputModel input = this.examsService.GetById<EditExamInputModel>(id);
            input.CourseItems = this.coursesService.GetAllAsSelectListItems();
            input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
            input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExamInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CourseItems = this.coursesService.GetAllAsSelectListItems();
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
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
        [Breadcrumb("All Exams", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult All(string input, int id = 1)
        {
            const int ItemsPerPage = 5;

            AllExamsListViewModel viewModel = new AllExamsListViewModel
            {
                ActiveCoursesCount = this.examsService.GetAllExamsCount(input),
                PageNumber = id,
                ItemsPerPage = ItemsPerPage,
                Exams = this.examsService.GetAll<AllExamsViewModel>(id, input, ItemsPerPage),
                Name = input,
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb("My Exams", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> AllByUser(string input, int id = 1)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            const int ItemsPerPage = 6;
            AllExamsByUserIdListViewModel viewModel = new AllExamsByUserIdListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ActiveCoursesCount = this.examsService.GetExamsCountByUserId(user.Id, input),
                Exams = this.examsService.GetAllByUserId<AllExamsByUserIdViewModel>(id, user.Id, input, ItemsPerPage),
                Subjects = this.subjectsService.GetAll<AllSubjectsViewModel>(),
                Name = input,
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb("My Exams", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> AllByUserAndSubjectId(int subjectId, string input, int id = 1)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            const int ItemsPerPage = 6;
            AllExamsByUserIdListViewModel viewModel = new AllExamsByUserIdListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ActiveCoursesCount = this.examsService.GetExamsCountByUserIdAndSubjectId(subjectId, user.Id, input),
                Exams = this.examsService.GetAllByUserIdAndSubjectId<AllExamsByUserIdViewModel>(subjectId, id, user.Id, input, ItemsPerPage),
                Subjects = this.subjectsService.GetAll<AllSubjectsViewModel>(),
                Name = input,
                SubjectId = subjectId,
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb("My Results", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> MyResults(string input, int id = 1)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            const int ItemsPerPage = 5;
            ResultFromExamListViewModel viewModel = new ResultFromExamListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ActiveCoursesCount = this.examsService.GetResultsCountByUserId(user.Id, input),
                Exams = this.examsService.GetAllByCurrentUserId<ResultFromExamViewModel>(id, user.Id, input, ItemsPerPage),
                Name = input,
            };

            foreach (var item in viewModel.Exams)
            {
                item.Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(item.ExamId, 1, string.Empty);
                item.Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(item.ExamId, user.Id);
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = "Lecturer, Administrator")]
        [Breadcrumb("Exam Details", FromAction = "All", FromController = typeof(ExamsController))]
        public IActionResult Details(int id)
        {
            ExamDetailsViewModel viewModel = this.examsService.GetById<ExamDetailsViewModel>(id);

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb("Take Exam", FromAction = "AllByUser")]
        public async Task<IActionResult> TakeExam(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (!this.examsService.IsExamActive(id))
            {
                this.TempData["Alert"] = "This exam is not active!";
                return this.RedirectToAction(nameof(this.AllByUser));
            }
            else
            {
                if (this.examsService.HasUserMadeCertainExam(id, user.Id))
                {
                    this.TempData["Alert"] = "You have already done this exam!";
                    return this.RedirectToAction(nameof(this.AllByUser));
                }
                else
                {
                    TakeExamInputModel input = new TakeExamInputModel
                    {
                        ExamId = id,
                        StartDate = this.examsService.GetStartDateById(id),
                        Duration = this.examsService.GetDurationById(id),
                        Name = this.examsService.GetNameById(id),
                        Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(id, 1, string.Empty),
                        Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(id, user.Id),
                    };

                    return this.View(input);
                }
            }
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [HttpPost]
        public async Task<IActionResult> TakeExam(int id, IFormCollection formCollection)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (this.examsService.HasUserMadeCertainExam(id, user.Id))
            {
                this.TempData["Alert"] = "You have already done this exam!";
                return this.RedirectToAction(nameof(this.AllByUser));
            }
            else
            {
                await this.examsService.TakeExamAsync(id, user.Id, formCollection);
                ResultFromExamViewModel viewModel = this.examsService.GetByExamIdAndUserId<ResultFromExamViewModel>(user.Id, id);
                viewModel.Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(id, 1, string.Empty);
                viewModel.Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(id, user.Id);
                int usersCountOnCertainExam = this.examsService.GetCountOfAllUsersWhoPassedCertainExam(id);
                double usersCountWithLowerGradesOnCertainExam = this.examsService.GetCountOfUsersWithLowerGradesOnCertainExam(id, viewModel.Grade);
                viewModel.CompareRateInPercents = ((double)(usersCountWithLowerGradesOnCertainExam / usersCountOnCertainExam)) * 100;

                return this.View("Result", viewModel);
            }
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> AddToCertificate(int courseId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            AddExamToCertificateInputModel input = new AddExamToCertificateInputModel
            {
                Exams = this.examsService.GetAllExamsAsSelectListItemsByCreatorId(user.Id),
                RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                CourseId = courseId,
            };

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddToCertificate(AddExamToCertificateInputModel input, int courseId)
        {
            if (this.examsService.CheckAlreadyUsedExam(input.ExamId, courseId))
            {
                this.TempData["Alert"] = "Exam has already been used!";
                return this.RedirectToAction("ById", "Courses", new { id = courseId });
            }

            if (this.examsService.IsExamCertificated(input.ExamId))
            {
                this.TempData["Alert"] = "Exam has already been certificated!";
                return this.RedirectToAction("ById", "Courses", new { id = courseId });
            }
            else
            {
                await this.examsService.AddExamToCertificateAsync(input);
                this.TempData["Message"] = "Exam has been successfully certificated!";
                return this.RedirectToAction("ById", "Courses", new { id = courseId });
            }
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [Breadcrumb("Review", FromAction = "MyResults")]
        public async Task<IActionResult> Review(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            ResultFromExamViewModel viewModel = this.examsService.GetByExamIdAndUserId<ResultFromExamViewModel>(user.Id, id);
            viewModel.Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(id, 1, string.Empty);
            viewModel.Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(id, user.Id);
            int usersCountOnCertainExam = this.examsService.GetCountOfAllUsersWhoPassedCertainExam(id);
            double usersCountWithLowerGradesOnCertainExam = this.examsService.GetCountOfUsersWithLowerGradesOnCertainExam(id, viewModel.Grade);
            viewModel.CompareRateInPercents = ((double)(usersCountWithLowerGradesOnCertainExam / usersCountOnCertainExam)) * 100;
            await this.examsService.MarkAsSeenAsync(user.Id, id);

            return this.View("Result", viewModel);
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> StartCertificate(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (!this.examsService.CanStartCertificate(id, user.Id))
            {
                this.TempData["Alert"] = "Cannot start certificate until you complete all lectures in this course!";
                return this.RedirectToAction("ById", "Courses", new { id });
            }
            else
            {
                int examId = this.examsService.GetCertificatedExamIdByCourse(id);

                if (examId == 0)
                {
                    this.TempData["Alert"] = "There is still no exam for certification!";
                    return this.RedirectToAction("ById", "Courses", new { id });
                }

                return this.RedirectToAction(nameof(this.TakeExam), new { id = examId });
            }
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        [HttpPost]
        public async Task<IActionResult> SaveAnswer(int id, int page, IFormCollection formCollection)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.examsService.SaveAnswerAsync(user.Id, formCollection);
            return this.RedirectToAction(nameof(this.TakeExam), new { id, page });
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> ExportMyExamsAsCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Type,Course,Description,PassMarks,TotalMarks");

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllExamsByUserIdListViewModel viewModel = new AllExamsByUserIdListViewModel
            {
                Exams = this.examsService.GetAllByUserId<AllExamsByUserIdViewModel>(1, user.Id, string.Empty, int.MaxValue),
            };

            foreach (var exam in viewModel.Exams)
            {
                sb.AppendLine($"{exam.Name},{exam.ExamType},{exam.CourseName},{exam.CourseDescription},{exam.PassMarks},{exam.TotalMarks}");
            }

            return this.File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "MyExamsInfo.csv");
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> ExportMyExamsAsExcel()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllExamsByUserIdListViewModel viewModel = new AllExamsByUserIdListViewModel
            {
                Exams = this.examsService.GetAllByUserId<AllExamsByUserIdViewModel>(1, user.Id, string.Empty, int.MaxValue),
            };

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MyExams");
                int currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "Type";
                worksheet.Cell(currentRow, 3).Value = "Course";
                worksheet.Cell(currentRow, 4).Value = "Description";
                worksheet.Cell(currentRow, 5).Value = "PassMarks";
                worksheet.Cell(currentRow, 6).Value = "TotalMarks";

                foreach (var exam in viewModel.Exams)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = exam.Name;
                    worksheet.Cell(currentRow, 2).Value = exam.ExamType;
                    worksheet.Cell(currentRow, 3).Value = exam.CourseName;
                    worksheet.Cell(currentRow, 4).Value = exam.CourseDescription;
                    worksheet.Cell(currentRow, 5).Value = exam.PassMarks;
                    worksheet.Cell(currentRow, 6).Value = exam.TotalMarks;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return this.File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyExamsInfo.xlsx");
                }
            }
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> ExportAllExamsAsCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Course,Lecturer,StartDate,EndDate");

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllExamsListViewModel viewModel = new AllExamsListViewModel
            {
                Exams = this.examsService.GetAll<AllExamsViewModel>(1, string.Empty, int.MaxValue),
            };

            foreach (var exam in viewModel.Exams)
            {
                sb.AppendLine($"{exam.Name},{exam.CourseName},{exam.LecturerFirstName + ' ' + exam.LecturerLastName},{exam.StartDate},{exam.EndDate}");
            }

            return this.File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "AllExamsInfo.csv");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> ExportAllExamsAsExcel()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllExamsListViewModel viewModel = new AllExamsListViewModel
            {
                Exams = this.examsService.GetAll<AllExamsViewModel>(1, string.Empty, int.MaxValue),
            };

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MyExams");
                int currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "Course";
                worksheet.Cell(currentRow, 3).Value = "Lecturer";
                worksheet.Cell(currentRow, 4).Value = "StartDate";
                worksheet.Cell(currentRow, 5).Value = "EndDate";

                foreach (var exam in viewModel.Exams)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = exam.Name;
                    worksheet.Cell(currentRow, 2).Value = exam.CourseName;
                    worksheet.Cell(currentRow, 3).Value = exam.LecturerFirstName + ' ' + exam.LecturerLastName;
                    worksheet.Cell(currentRow, 4).Value = exam.StartDate;
                    worksheet.Cell(currentRow, 5).Value = exam.EndDate;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return this.File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllExamsInfo.xlsx");
                }
            }
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> ExportMyResultsAsCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Result,Points,Correct Answers,Course,Active from,Time,Status,Active to,Seen on");

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            IEnumerable<ResultFromExamViewModel> viewModel = this.examsService.GetAllByCurrentUserId<ResultFromExamViewModel>(1, user.Id, string.Empty, int.MaxValue);

            foreach (var item in viewModel)
            {
                item.Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(item.ExamId, 1, string.Empty);
                item.Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(item.ExamId, user.Id);
            }

            foreach (var exam in viewModel)
            {
                string seenOn = exam.SeenOn.HasValue ? exam.SeenOn.Value.ToString() : "Not seen yet";
                sb.AppendLine($"{exam.ExamName},{exam.SuccessRate.ToString("f2")},{exam.PointsEarned},{exam.CorrectAnswers},{exam.ExamCourseName},{exam.ExamStartDate},{exam.TimeSpent},{exam.Status.ToString()},{exam.ExamEndDate},{seenOn}");
            }

            return this.File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "MyResultsInfo.csv");
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> ExportMyResultsAsExcel()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            IEnumerable<ResultFromExamViewModel> viewModel = this.examsService.GetAllByCurrentUserId<ResultFromExamViewModel>(1, user.Id, string.Empty, int.MaxValue);

            foreach (var item in viewModel)
            {
                item.Questions = this.questionsService.GetAllByExam<AllQuestionsByExamViewModel>(item.ExamId, 1, string.Empty);
                item.Answers = this.answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(item.ExamId, user.Id);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MyExams");
                int currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "Result";
                worksheet.Cell(currentRow, 3).Value = "Points";
                worksheet.Cell(currentRow, 4).Value = "Correct Answers";
                worksheet.Cell(currentRow, 5).Value = "Course";
                worksheet.Cell(currentRow, 6).Value = "Active from";
                worksheet.Cell(currentRow, 7).Value = "Time";
                worksheet.Cell(currentRow, 8).Value = "Status";
                worksheet.Cell(currentRow, 9).Value = "Active to";
                worksheet.Cell(currentRow, 10).Value = "Seen to";

                foreach (var exam in viewModel)
                {
                    string seenOn = exam.SeenOn.HasValue ? exam.SeenOn.Value.ToString() : "Not seen yet";

                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = exam.ExamName;
                    worksheet.Cell(currentRow, 2).Value = exam.SuccessRate.ToString("f2");
                    worksheet.Cell(currentRow, 3).Value = exam.PointsEarned;
                    worksheet.Cell(currentRow, 4).Value = exam.CorrectAnswers;
                    worksheet.Cell(currentRow, 5).Value = exam.ExamCourseName;
                    worksheet.Cell(currentRow, 6).Value = exam.ExamStartDate;
                    worksheet.Cell(currentRow, 7).Value = exam.TimeSpent;
                    worksheet.Cell(currentRow, 8).Value = exam.Status.ToString();
                    worksheet.Cell(currentRow, 9).Value = exam.ExamEndDate;
                    worksheet.Cell(currentRow, 10).Value = seenOn;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return this.File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyResultsInfo.xlsx");
                }
            }
        }
    }
}
