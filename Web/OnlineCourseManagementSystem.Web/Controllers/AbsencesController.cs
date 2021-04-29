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
    using OnlineCourseManagementSystem.Web.ViewModels.Absences;
    using OnlineCourseManagementSystem.Web.ViewModels.Students;

    public class AbsencesController : Controller
    {
        private readonly IAbsencesService absencesService;
        private readonly IStudentsService studentsService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public AbsencesController(
            IAbsencesService absencesService,
            IStudentsService studentsService,
            ICoursesService coursesService,
            UserManager<ApplicationUser> userManager)
        {
            this.absencesService = absencesService;
            this.studentsService = studentsService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.ParentRoleName)]
        public async Task<IActionResult> AllByStudentInInterval()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            GetAllByStudentInIntervalInputModel input = new GetAllByStudentInIntervalInputModel
            {
                StudentItems = this.studentsService.GetAllByParentAsSelectListItems(user.ParentId),
                CourseItems = this.coursesService.GetAllAsSelectListItems(),
            };

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.ParentRoleName)]
        [HttpPost]
        public async Task<IActionResult> AllByStudentInInterval(GetAllByStudentInIntervalInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.StudentItems = this.studentsService.GetAllByParentAsSelectListItems(user.Id);
                input.CourseItems = this.coursesService.GetAllAsSelectListItems();
                return this.View(input);
            }

            GetAllByStudentInIntervalListViewModel viewModel = new GetAllByStudentInIntervalListViewModel
            {
                Students = this.absencesService.GetAllByStudentInSpecifiedRange<GetAllByStudentInIntervalViewModel>(input),
                StartDate = input.StartDate,
                EndDate = input.EndDate,
            };

            this.ViewBag.CourseName = this.coursesService.CourseNameByStudentAndCourse(input.StudentId, input.CourseId);

            return this.View("AllByStudentInIntervalResult", viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult AllByCourseAndLecture(int courseId, int lectureId, string courseName, string lectureTitle)
        {
            AllAbsencesByCourseAndLectureListViewModel viewModel = new AllAbsencesByCourseAndLectureListViewModel
            {
                CourseName = courseName,
                LectureTitle = lectureTitle,
                CourseId = courseId,
                LectureId = lectureId,
                Absences = this.absencesService.GetAllByCourseAndLecture<AllAbsencesByCourseAndLectureViewModel>(courseId, lectureId),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Create(int courseId, string courseName, int lectureId, string lectureTitle)
        {
            CreateAbsenceListInputModel input = new CreateAbsenceListInputModel
            {
                Students = this.studentsService.GetAllByCourse<AllStudentsByCourseViewModel>(courseId),
            };

            input.CourseId = courseId;
            input.CourseName = courseName;
            input.LectureId = lectureId;
            input.LectureTitle = lectureTitle;

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAbsenceListInputModel input, int courseId, string courseName, int lectureId, string lectureTitle)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseId = courseId;
                input.CourseName = courseName;
                input.LectureId = lectureId;
                input.LectureTitle = lectureTitle;
                input.Students = this.studentsService.GetAllByCourse<AllStudentsByCourseViewModel>(courseId);
                return this.View(input);
            }

            input.CourseId = courseId;
            input.CourseName = courseName;
            input.LectureId = lectureId;
            input.LectureTitle = lectureTitle;
            input.Students = this.studentsService.GetAllByCourse<AllStudentsByCourseViewModel>(courseId);
            try
            {
                await this.absencesService.CreateAsync(input);
                this.TempData["SuccessMessage"] = "You create successfully attendance/s!";
            }
            catch (Exception ex)
            {
                this.TempData["ErrorMessage"] = ex.Message;
            }

            return this.RedirectToAction(nameof(this.AllByCourseAndLecture), new { courseId, lectureId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult EditMultiple(int courseId, int lectureId, string courseName, string lectureTitle)
        {
            IEnumerable<EditAbsenceInputModel> inputs = this.absencesService.GetAllByCourseAndLecture<EditAbsenceInputModel>(courseId, lectureId);
            this.ViewBag.CourseId = courseId;
            this.ViewBag.CourseName = courseName;
            this.ViewBag.LectureTitle = lectureTitle;
            this.ViewBag.LectureId = lectureId;

            return this.View(inputs);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> EditMultiple(IEnumerable<EditAbsenceInputModel> inputs, int courseId, int lectureId, string courseName, string lectureTitle)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.CourseId = courseId;
                this.ViewBag.CourseName = courseName;
                this.ViewBag.LectureTitle = lectureTitle;
                this.ViewBag.LectureId = lectureId;
                return this.View(inputs);
            }

            this.ViewBag.CourseId = courseId;
            this.ViewBag.CourseName = courseName;
            this.ViewBag.LectureTitle = lectureTitle;
            this.ViewBag.LectureId = lectureId;
            await this.absencesService.UpdateMultipleAsync(inputs);

            return this.RedirectToAction(nameof(this.AllByCourseAndLecture), new { courseId, lectureId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult Edit(int id)
        {
            EditAbsenceInputModel input = this.absencesService.GetById<EditAbsenceInputModel>(id);
            this.ViewBag.CourseId = input.CourseId;
            this.ViewBag.LectureId = input.LectureId;
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAbsenceInputModel input, int id, int courseId, int lectureId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.absencesService.UpdateAsync(input);

            return this.RedirectToAction(nameof(this.AllByCourseAndLecture), new { courseId, lectureId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> DeleteByLecture(int courseId, int lectureId)
        {
            await this.absencesService.DeleteAsyncForSpecificLecture(courseId, lectureId);
            this.TempData["ErrorMessage"] = "You deleted attendance/s successfully!";
            return this.RedirectToAction(nameof(this.AllByCourseAndLecture), new { courseId, lectureId });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> DeleteByStudent(int courseId, int lectureId, string studentId)
        {
            await this.absencesService.DeleteAsyncForSpecificStudent(courseId, lectureId, studentId);
            this.TempData["ErrorMessage"] = "You deleted attendance successfully!";
            return this.RedirectToAction(nameof(this.AllByCourseAndLecture), new { courseId, lectureId });
        }
    }
}
