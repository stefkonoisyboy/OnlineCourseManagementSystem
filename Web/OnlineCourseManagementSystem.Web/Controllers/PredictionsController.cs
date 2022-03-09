namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Data;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class PredictionsController : Controller
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<UserCourse> userCoursesRepository;
        private readonly ApplicationDbContext dbContext;

        public PredictionsController(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<UserCourse> userCoursesRepository,
            ApplicationDbContext dbContext)
        {
            this.usersRepository = usersRepository;
            this.coursesRepository = coursesRepository;
            this.userCoursesRepository = userCoursesRepository;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> SeedAsync()
        {
            string coursesFilePath = Path.Combine("Datasets", "courses.csv");
            string userInCoursesFilePath = Path.Combine("Datasets", "userInCourses.csv");

            StreamReader coursesReader = new StreamReader(coursesFilePath);
            StreamReader userInCoursesReader = new StreamReader(userInCoursesFilePath);

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
                await this.coursesRepository.SaveChangesAsync();

                courseLine = coursesReader.ReadLine();
            }

            coursesReader.Close();

            return this.Redirect("/");
        }
    }
}
