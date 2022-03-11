namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Services.Data.MachineLearning;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
    using OnlineCourseManagementSystem.Web.ViewModels.Skills;
    using OnlineCourseManagementSystem.Web.ViewModels.Subjects;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;
    using SmartBreadcrumbs.Nodes;

    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly ITagsService tagsService;
        private readonly ISubjectsService subjectsService;
        private readonly ILecturersService lecturersService;
        private readonly ISkillsService skillsService;
        private readonly IReviewsService reviewsService;
        private readonly IUsersService usersService;
        private readonly IFilesService filesService;
        private readonly ILecturesService lecturesService;
        private readonly ICompletitionsService completitionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            ICoursesService coursesService,
            ITagsService tagsService,
            ISubjectsService subjectsService,
            ILecturersService lecturersService,
            ISkillsService skillsService,
            IReviewsService reviewsService,
            IUsersService usersService,
            IFilesService filesService,
            ILecturesService lecturesService,
            ICompletitionsService completitionsService,
            UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.tagsService = tagsService;
            this.subjectsService = subjectsService;
            this.lecturersService = lecturersService;
            this.skillsService = skillsService;
            this.reviewsService = reviewsService;
            this.usersService = usersService;
            this.filesService = filesService;
            this.lecturesService = lecturesService;
            this.completitionsService = completitionsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> AllRecommendedByCurrentUser(int id = 1, string recommendationType = "Highly")
        {
            const int ItemsPerPage = 12;
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            string modelFile = "OMCSCourses.zip";

            if (!System.IO.File.Exists("OMCSCourses.zip"))
            {
                Recommender.TrainModel(System.IO.Path.Combine("Datasets", "userInCourses.csv"), modelFile);
            }

            IEnumerable<UserInCourse> testModelData = this.coursesService.GetAllForTestingAIByUserId(id, user.Id, ItemsPerPage);
            IEnumerable<UserInCourseScore> result = Recommender.TestModel(modelFile, testModelData);

            for (int i = 0; i < result.Count(); i++)
            {
                if (result.ToList()[i].Score >= 0.75)
                {
                    testModelData.ToList()[i].RecommendationType = "Highly";
                }
                else if (result.ToList()[i].Score >= 0.50 && result.ToList()[i].Score < 0.75)
                {
                    testModelData.ToList()[i].RecommendationType = "Strongly";
                }
                else if (result.ToList()[i].Score >= 0.25 && result.ToList()[i].Score < 0.50)
                {
                    testModelData.ToList()[i].RecommendationType = "Recommended";
                }
                else if (result.ToList()[i].Score < 0.25)
                {
                    testModelData.ToList()[i].RecommendationType = "NotRecommended";
                }
            }

            if (recommendationType == "Highly")
            {
                result = result.Where(x => x.Score >= 0.75).ToList();
            }
            else if (recommendationType == "Strongly")
            {
                result = result.Where(x => x.Score >= 0.50 && x.Score < 0.75).ToList();
            }
            else if (recommendationType == "Recommended")
            {
                result = result.Where(x => x.Score >= 0.25 && x.Score < 0.50).ToList();
            }
            else if (recommendationType == "NotRecommended")
            {
                result = result.Where(x => x.Score < 0.25).ToList();
            }

            AllRecommendedCoursesByAIViewModel<UserInCourse, UserInCourseScore> viewModel = new AllRecommendedCoursesByAIViewModel<UserInCourse, UserInCourseScore>
            {
                InputData = testModelData.Where(x => x.RecommendationType == recommendationType).ToList(),
                OutputData = result.Skip((id - 1) * ItemsPerPage).Take(ItemsPerPage),
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ActiveCoursesCount = result.Count(),
                RecommendationType = recommendationType,
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = "Student,Lecturer,Administrator")]
        public async Task<IActionResult> ById(int id, int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 3;

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            CourseDetailsViewModel viewModel = this.coursesService.GetById<CourseDetailsViewModel>(id);

            viewModel.Skills = this.skillsService.GetAllByCourseId<AllSkillsByCourseIdViewModel>(id);
            viewModel.Reviews = this.reviewsService.GetAllByCourseId<AllReviewsByCourseIdViewModel>(id);
            viewModel.Lecturers = this.lecturersService.GetAllByCourseId<AllLecturersByCourseIdViewModel>(id);
            viewModel.CompletedLecturesCount = this.completitionsService.GetAllCompletitionsCountByCourseIdAndUserId(id, user.Id);
            viewModel.ListOfLectures = new AllLecturesByIdListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ActiveCoursesCount = this.lecturesService.GetLecturesCountById(id),
                Lectures = this.lecturesService.GetAllById<AllLecturesByIdViewModel>(id, page, ItemsPerPage),
                CourseId = id,
            };
            viewModel.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);

            BreadcrumbNode mycoursesNode = new MvcBreadcrumbNode("AllByCurrentUser", "Courses", "My Courses");

            BreadcrumbNode courseDetailsNode = new MvcBreadcrumbNode("ById", "Courses", "Course Details")
            {
                Parent = mycoursesNode,
                RouteValues = new { id = id },
            };

            this.ViewData["BreadcrumbNode"] = courseDetailsNode;

            return this.View(viewModel);
        }

        [Authorize]
        [Breadcrumb(" Course Details ", FromAction = "AllUpcomingAndActive")]
        public async Task<IActionResult> Details(int id, int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 3;

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            CourseDetailsViewModel viewModel = this.coursesService.GetById<CourseDetailsViewModel>(id);

            viewModel.Skills = this.skillsService.GetAllByCourseId<AllSkillsByCourseIdViewModel>(id);
            viewModel.Reviews = this.reviewsService.GetAllByCourseId<AllReviewsByCourseIdViewModel>(id);
            viewModel.Lecturers = this.lecturersService.GetAllByCourseId<AllLecturersByCourseIdViewModel>(id);
            viewModel.ListOfLectures = new AllLecturesByIdListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ActiveCoursesCount = this.lecturesService.GetLecturesCountById(id),
                Lectures = this.lecturesService.GetAllById<AllLecturesByIdViewModel>(id, page, ItemsPerPage),
                CourseId = id,
            };
            viewModel.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult All()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAll<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [Breadcrumb("My Courses", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> AllByCurrentUser(int id = 1)
        {
            if (this.User.IsInRole(GlobalConstants.LecturerRoleName))
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);
                const int ItemsPerPage = 6;
                UpcomingAndActiveCoursesViewModel viewModel = new UpcomingAndActiveCoursesViewModel
                {
                    ListOfActiveCourses = new AllActiveCoursesListViewModel
                    {
                        ItemsPerPage = ItemsPerPage,
                        PageNumber = id,
                        ActiveCoursesCount = this.coursesService.GetAllCoursesByCreatorIdCount(user.Id),
                        ActiveCourses = this.coursesService.GetAllByCreatorId<AllActiveCoursesViewModel>(id, user.Id, ItemsPerPage),
                    },
                    Subjects = this.subjectsService.GetAll<AllSubjectsViewModel>(),
                };

                this.ViewData["CurrentUserHeading"] = "Messages";

                return this.View(viewModel);
            }
            else
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);
                const int ItemsPerPage = 6;
                UpcomingAndActiveCoursesViewModel viewModel = new UpcomingAndActiveCoursesViewModel
                {
                    ListOfActiveCourses = new AllActiveCoursesListViewModel
                    {
                        ItemsPerPage = ItemsPerPage,
                        PageNumber = id,
                        ActiveCoursesCount = this.coursesService.GetAllCoursesByUserIdCount(user.Id),
                        ActiveCourses = this.coursesService.GetAllByUser<AllActiveCoursesViewModel>(id, user.Id, ItemsPerPage),
                    },
                    Subjects = this.subjectsService.GetAll<AllSubjectsViewModel>(),
                };

                this.ViewData["CurrentUserHeading"] = "Messages";

                return this.View(viewModel);
            }
        }

        [Authorize]
        [Breadcrumb("My Courses", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> AllByCurrentUserAndSubjectId(int subjectId, int id = 1)
        {
            if (this.User.IsInRole(GlobalConstants.LecturerRoleName))
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);
                const int ItemsPerPage = 6;
                UpcomingAndActiveCoursesViewModel viewModel = new UpcomingAndActiveCoursesViewModel
                {
                    ListOfActiveCourses = new AllActiveCoursesListViewModel
                    {
                        ItemsPerPage = ItemsPerPage,
                        PageNumber = id,
                        ActiveCoursesCount = this.coursesService.GetAllCoursesByCreatorIdAndSubjectIdCount(subjectId, user.Id),
                        ActiveCourses = this.coursesService.GetAllByCreatorIdAndSubjectId<AllActiveCoursesViewModel>(id, user.Id, subjectId, ItemsPerPage),
                        SubjectId = subjectId,
                    },
                    Subjects = this.subjectsService.GetAll<AllSubjectsViewModel>(),
                };

                this.ViewData["CurrentUserHeading"] = "Messages";

                return this.View(viewModel);
            }
            else
            {
                ApplicationUser user = await this.userManager.GetUserAsync(this.User);
                const int ItemsPerPage = 6;
                UpcomingAndActiveCoursesViewModel viewModel = new UpcomingAndActiveCoursesViewModel
                {
                    ListOfActiveCourses = new AllActiveCoursesListViewModel
                    {
                        ItemsPerPage = ItemsPerPage,
                        PageNumber = id,
                        ActiveCoursesCount = this.coursesService.GetAllCoursesByUserIdAndSubjectIdCount(subjectId, user.Id),
                        ActiveCourses = this.coursesService.GetAllByUserAndSubject<AllActiveCoursesViewModel>(id, user.Id, subjectId, ItemsPerPage),
                        SubjectId = subjectId,
                    },
                    Subjects = this.subjectsService.GetAll<AllSubjectsViewModel>(),
                };

                this.ViewData["CurrentUserHeading"] = "Messages";

                return this.View(viewModel);
            }
        }

        [Authorize]
        [Breadcrumb(" Upcoming and Active Courses ", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult AllUpcomingAndActive(string name = null, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            UpcomingAndActiveCoursesViewModel viewModel = new UpcomingAndActiveCoursesViewModel
            {
                ListOfActiveCourses = new AllActiveCoursesListViewModel
                {
                    ItemsPerPage = ItemsPerPage,
                    PageNumber = id,
                    ActiveCoursesCount = this.coursesService.GetAllActiveCoursesCount(name),
                    ActiveCourses = this.coursesService.GetAllActive<AllActiveCoursesViewModel>(id, name, ItemsPerPage),
                    Name = name,
                },
                UpcomingCourses = this.coursesService.GetAllUpcoming<AllUpcomingCoursesViewModel>(),
                Tags = this.tagsService.GetAll<AllTagsViewModel>(),
                Subjects = this.subjectsService.GetAll<AllSubjectsViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [Breadcrumb(" Upcoming and Active Courses ", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult AllUpcomingAndActiveBySubjectId(int subjectId, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            UpcomingAndActiveCoursesViewModel viewModel = new UpcomingAndActiveCoursesViewModel
            {
                ListOfActiveCourses = new AllActiveCoursesListViewModel
                {
                    ItemsPerPage = ItemsPerPage,
                    PageNumber = id,
                    ActiveCoursesCount = this.coursesService.GetAllActiveCoursesBySubjectIdCount(subjectId),
                    ActiveCourses = this.coursesService.GetAllActiveBySubjectId<AllActiveCoursesViewModel>(id, subjectId, ItemsPerPage),
                    SubjectId = subjectId,
                },
                UpcomingCourses = this.coursesService.GetAllUpcoming<AllUpcomingCoursesViewModel>(),
                Tags = this.tagsService.GetAll<AllTagsViewModel>(),
                Subjects = this.subjectsService.GetAll<AllSubjectsViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AllUnapproved()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllUnapproved<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllByUser(string id)
        {
            AllCoursesByUserListViewModel viewModel = new AllCoursesByUserListViewModel
            {
                Courses = this.coursesService.GetAllByUser<AllCoursesByUserViewModel>(1, id, 6),
            };

            foreach (var course in viewModel.Courses)
            {
                course.CompletedLecturesCount = this.completitionsService.GetAllCompletitionsCountByCourseIdAndUserId(course.CourseId, id);
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("My Courses", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> AllByCurrentLecturer(int id = 1)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            const int ItemsPerPage = 6;
            AllCoursesByCurrentLecturerListViewModel viewModel = new AllCoursesByCurrentLecturerListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                LecturerCoursesCount = this.coursesService.GetAllCoursesByCreatorIdCount(user.Id),
                Courses = this.coursesService.GetAllByCreatorId<AllCoursesByCurrentLecturerViewModel>(id, user.Id, ItemsPerPage),
                FeaturedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllUpcoming()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllUpcoming<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllPast()
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllPast<AllCoursesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AllByTag(SearchByTagInputModel input)
        {
            AllCoursesListViewModel viewModel = new AllCoursesListViewModel
            {
                Courses = this.coursesService.GetAllByTag<AllCoursesViewModel>(input),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("Add Meta", FromAction = "Create")]
        public async Task<IActionResult> Meta()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            CreateMetaInputModel input = new CreateMetaInputModel
            {
                SubjectsItems = this.subjectsService.GetAllAsSelectListItems(),
                TagsItems = this.tagsService.GetAllAsSelectListItems(),
                LecturersItems = this.lecturersService.GetAllAsSelectListItems(),
                RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                CoursesItems = this.coursesService.GetAllAsSelectListItemsByCreatorId(user.Id),
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Meta(CreateMetaInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.SubjectsItems = this.subjectsService.GetAllAsSelectListItems();
                input.TagsItems = this.tagsService.GetAllAsSelectListItems();
                input.LecturersItems = this.lecturersService.GetAllAsSelectListItems();
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                input.CoursesItems = this.coursesService.GetAllAsSelectListItemsByCreatorId(user.Id);
                return this.View(input);
            }

            input.UserId = user.Id;

            try
            {
                await this.coursesService.CreateMetaAsync(input);
                this.TempData["Message"] = "Meta information about the course successfully created!";
            }
            catch (Exception ex)
            {
                this.TempData["AlertMessage"] = ex.Message;
            }

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.RedirectToAction("AllLecturesByCreatorId", "Lectures");
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [Breadcrumb("Create a Course", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            CreateCourseInputModel input = new CreateCourseInputModel
            {
                RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>(),
                CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
            };

            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.View(input);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Create(CreateCourseInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                return this.View(input);
            }

            input.UserId = user.Id;
            input.CreatorId = user.Id;
            await this.coursesService.CreateAsync(input);
            this.TempData["Message"] = "Course is created successfully!";
            this.ViewData["CurrentUserHeading"] = "Messages";

            return this.RedirectToAction(nameof(this.Meta));
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            EditCourseInputModel input = this.coursesService.GetById<EditCourseInputModel>(id);
            input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
            input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseInputModel input, int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                return this.View(input);
            }

            input.Id = id;
            await this.coursesService.UpdateAsync(input);
            this.TempData["Message"] = "Course updated successfully!";

            return this.RedirectToAction("EditMeta", "Courses", new { id });
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public async Task<IActionResult> EditMeta(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            EditMetaInputModel input = this.coursesService.GetById<EditMetaInputModel>(id);
            input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
            input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
            input.SubjectsItems = this.subjectsService.GetAllAsSelectListItems();

            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        [HttpPost]
        public async Task<IActionResult> EditMeta(int id, int fileId, EditMetaInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id);
                input.RecommendedCourses = this.coursesService.GetAllRecommended<AllRecommendedCoursesByIdViewModel>();
                input.SubjectsItems = this.subjectsService.GetAllAsSelectListItems();
                input.FileRemoteUrl = this.filesService.GetRemoteUrlById(fileId);
                return this.View(input);
            }

            input.Id = id;
            input.FileId = fileId;
            await this.coursesService.UpdateMetaAsync(input);
            this.TempData["Message"] = "Course meta data updated successfully!";

            return this.RedirectToAction("AllLecturesByCreatorId", "Lectures");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.coursesService.DeleteAsync(id);
            this.TempData["Message"] = "Course deleted successfully!";
            return this.Redirect("/");
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Approve(int id)
        {
            await this.coursesService.ApproveAsync(id);
            this.TempData["Message"] = "Course approved successfully!";

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult AllCoursesByCourseNameAndSubject(int subjectId, string name)
        {
            AllCoursesBySubjectAndCourseNameViewModel viewModel = new AllCoursesBySubjectAndCourseNameViewModel()
            {
                CourseName = name,
                LastActiveCourse = this.coursesService.GetLastActiveCourseBySubjectId<LastActiveCourseViewModel>(subjectId),
                Courses = this.coursesService.GetBySubjectAndCourseName<AllCoursesBySubjectViewModel>(subjectId, name),
            };

            return this.View(viewModel);
        }

        // [Authorize]
        // public IActionResult AllCoursesByNameAndSubject(int subjectId, string name)
        // {
        //    AllActiveCoursesListViewModel viewModel = new AllActiveCoursesListViewModel()
        //    {
        //        s
        //    }
        // }
    }
}
