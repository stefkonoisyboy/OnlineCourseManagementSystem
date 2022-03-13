namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Subjects;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class SubjectsController : Controller
    {
        private readonly ISubjectsService subjectsService;
        private readonly ICoursesService coursesService;

        public SubjectsController(ISubjectsService subjectsService, ICoursesService coursesService)
        {
            this.subjectsService = subjectsService;
            this.coursesService = coursesService;
        }

        [Authorize]
        [Breadcrumb("RoadMaps", FromAction ="Index", FromController = typeof(HomeController))]
        public IActionResult AllSubjectsRoadMap()
        {
            AllSubjectsRoadMapViewModel allSubjectsRoadMap = new AllSubjectsRoadMapViewModel()
            {
                ProgrammingBasics = this.subjectsService.GetByName<SubjectViewModel>("Programming Basics"),
                Fundamentals = this.subjectsService.GetByName<SubjectViewModel>("Fundamentals"),
                CsharpSubjectsRoadMap = this.subjectsService.GetAllWithReferedCsharpCourses<SubjectRoadMapViewModel>(),
                JavaSubjectsRoadMap = this.subjectsService.GetAllWithReferedJavaCourses<SubjectRoadMapViewModel>(),
                JSSubjectRoadMap = this.subjectsService.GetAllWithReferedJSCourses<SubjectRoadMapViewModel>(),
            };

            return this.View(allSubjectsRoadMap);
        }

        [Authorize]
        public IActionResult ById(int subjectId, int id = 1)
        {
            SubjectDetailsViewModel viewModel = this.subjectsService.GetById<SubjectDetailsViewModel>(subjectId);
            viewModel.CurrentYearCourses = this.coursesService.GetAllCurrentYearBySubjectId<AllCoursesBySubjectViewModel>(subjectId);
            viewModel.CoursesBySubjectListViewModel = new AllCoursesBySubjectListViewModel()
            {
                PageNumber = id,
            };
            var query = this.coursesService.GetAllUnactiveCourses<AllCoursesBySubjectViewModel>(subjectId);
            viewModel.CoursesBySubjectListViewModel.PagesCount = (int)Math.Ceiling((double)query.ToList().Count() / viewModel.CoursesBySubjectListViewModel.ItemsPerPage);

            int skip = (id - 1) * viewModel.CoursesBySubjectListViewModel.ItemsPerPage;
            viewModel.CoursesBySubjectListViewModel.Courses = query
                                                        .Skip(skip)
                                                        .Take(viewModel.CoursesBySubjectListViewModel.ItemsPerPage);
            viewModel.CoursesBySubjectListViewModel.SubjectId = subjectId;
            BreadcrumbNode roadMapNode = new MvcBreadcrumbNode("All", "Trainings", "RoadMap");
            BreadcrumbNode byIdNode = new MvcBreadcrumbNode("ById", "Subjects", "Subject")
            {
                Parent = roadMapNode,
            };

            this.ViewData["BreadcrumbNode"] = byIdNode;
            return this.View(viewModel);
        }
    }
}
