namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages;
    using OnlineCourseManagementSystem.Web.ViewModels.ContactMessages;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Home;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using SmartBreadcrumbs.Attributes;

    public class HomeController : BaseController
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly IOrdersService ordersService;
        private readonly ICoursesService coursesService;
        private readonly IReviewsService reviewsService;
        private readonly IUsersService usersService;
        private readonly IContactMessagesService contactMessagesService;
        private readonly IChatbotMessagesService chatbotMessagesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<UserCourse> userCoursesRepository;
        private readonly ApplicationDbContext dbContext;

        public HomeController(
            IAssignmentsService assignmentsService,
            IOrdersService ordersService,
            ICoursesService coursesService,
            IReviewsService reviewsService,
            IUsersService usersService,
            IContactMessagesService contactMessagesService,
            IChatbotMessagesService chatbotMessagesService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<UserCourse> userCoursesRepository,
            ApplicationDbContext dbContext)
        {
            this.assignmentsService = assignmentsService;
            this.ordersService = ordersService;
            this.coursesService = coursesService;
            this.reviewsService = reviewsService;
            this.usersService = usersService;
            this.contactMessagesService = contactMessagesService;
            this.chatbotMessagesService = chatbotMessagesService;
            this.userManager = userManager;
            this.usersRepository = usersRepository;
            this.coursesRepository = coursesRepository;
            this.userCoursesRepository = userCoursesRepository;
            this.dbContext = dbContext;
        }

        [Authorize]
        [DefaultBreadcrumb(" Home ")]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (this.coursesRepository.All().Count() < 20)
            {
                string coursesFilePath = Path.Combine("Datasets", "courses.csv");

                StreamReader coursesReader = new StreamReader(coursesFilePath);

                string courseLine = coursesReader.ReadLine();
                while (courseLine != null)
                {
                    string[] tokens = courseLine.Split(',').ToArray();
                    int courseId = int.Parse(tokens[0]);
                    string courseName = tokens[1];

                    Course course = new Course
                    {
                        MachineLearningId = courseId,
                        Name = courseName,
                        Description = @"В модула ""C# Web"" се изучава основата на уеб-базираните технологии.
                С този модул се полагат основите на важни принципи заложени в ASP.NET Core. 
                Ще се разглеждат концепции от света на уеб разработката като HTTP, бисквитки и сесии. Курсистите имат възможността да изградят собствен сървър и MVC Framework, което им дава дълбоко разбиране за начина, по който работи едно уеб приложение. Изучават се технологията AJAX, кеширане, сигурност на уеб приложения, сокети, работа с библиотеки. По време на модула курсистите ще работят по практически лабораторни упражнения (лабове) и работилници за изграждане на цялостни, пълнофункционални ASP.NET Core уеб приложения. В края на модула курсистите изграждат свой проект с най-популярната работна рамка в света 
                на C# - ASP.Net Core и база данни MS SQL.",
                        StartDate = new DateTime(2021, 4, 17),
                        EndDate = new DateTime(2021, 7, 25),
                        IsApproved = true,
                        Lecturers = new List<CourseLecturer>
                    {
                        new CourseLecturer
                        {
                            LecturerId = this.dbContext.Lecturers.FirstOrDefault().Id,
                        },
                    },
                        Price = 200,
                        Subject = this.dbContext.Subjects.FirstOrDefault(s => s.Name == "Informatics"),
                        File = new OnlineCourseManagementSystem.Data.Models.File
                        {
                            Extension = "jpg",
                            RemoteUrl = "https://res.cloudinary.com/dm2yls09j/image/upload/v1618063399/courses-files/uw8x7mykwfut5czhakp7.jpg",
                        },
                        CreatorId = this.dbContext.Users.FirstOrDefault(u => u.UserName == "niki").Id,
                    };

                    await this.coursesRepository.AddAsync(course);

                    courseLine = coursesReader.ReadLine();
                }

                await this.coursesRepository.SaveChangesAsync();
                coursesReader.Close();
            }

            if (this.User.Identity.IsAuthenticated)
            {
                HomeViewModel homeViewModel = new HomeViewModel
                {
                    AssignmentsCount = this.assignmentsService.GetAllBy<AssignmentViewModel>(user.Id).Count(),
                    CoursesInCartCount = this.ordersService.CoursesInCartCount(user.Id),
                    LatestCourses = this.coursesService.GetTopLatest<TopLatestCoursesViewModel>(),
                    NextCourses = this.coursesService.GetTopNext<TopNextCoursesViewModel>(),
                    Reviews = this.reviewsService.GetTop3Recent<AllReviewsByCourseIdViewModel>(),
                    CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                    UserId = user.Id,
                    Chatbot = new AllChatbotMessagesByCreatorIdListViewModel
                    {
                        Messages = this.chatbotMessagesService.GetAllByCreatorId<AllChatbotMessagesByCreatorIdViewModel>(user.Id),
                    },
                };

                return this.View(homeViewModel);
            }
            else
            {
                HomeViewModel homeViewModel = new HomeViewModel
                {
                    Reviews = this.reviewsService.GetTop3Recent<AllReviewsByCourseIdViewModel>(),
                    CurrentUser = this.usersService.GetById<CurrentUserViewModel>(user.Id),
                    Chatbot = new AllChatbotMessagesByCreatorIdListViewModel
                    {
                        Messages = this.chatbotMessagesService.GetAllByCreatorId<AllChatbotMessagesByCreatorIdViewModel>(user.Id),
                    },
                };
                return this.View(homeViewModel);
            }
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [Breadcrumb(" Contact ", FromAction = "Index")]
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(CreateContactMessageInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.contactMessagesService.CreateAsync(inputModel);
            this.TempData["Success-ContactMessage-Create"] = "Successfully created contact message!";

            return this.View();
        }

        [Breadcrumb(" About ", FromAction = "Index")]
        public IActionResult About()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
