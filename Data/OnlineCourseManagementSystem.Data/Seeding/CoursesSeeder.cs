namespace OnlineCourseManagementSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;

    public class CoursesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Courses.Any())
            {
                return;
            }

            Course course1 = new Course
            {
                Name = "ASP.NET Core",
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
                        LecturerId = dbContext.Lecturers.FirstOrDefault().Id,
                    },
                },
                Price = 200,
                Subject = dbContext.Subjects.FirstOrDefault(s => s.Name == "Informatics"),
                File = new File
                {
                    Extension = "jpg",
                    RemoteUrl = "https://res.cloudinary.com/dm2yls09j/image/upload/v1618063399/courses-files/uw8x7mykwfut5czhakp7.jpg",
                },
                CreatorId = dbContext.Users.FirstOrDefault(u => u.UserName == "niki").Id,
            };

            Course course2 = new Course
            {
                Name = "Entity Farmewwork core",
                Description = @"Курсът проследява в детайли работата с ORM технологията:
                                Entity Framework Core (EF core), която е стандарт за ORM в C# и .NET Core приложения. 
                                EF Core позволява mapping между релационна база и обектно-ориентиран модел чрез подходите
                                ""database first"" и ""code first"" и предоставя мощно обектно-ориентирано API за заявки към базата данни и извършване на CRUD операции. EF core предоставя както допълнително ниво на абстракция,
                                така и лесен начин за обработка на данните от базата. В курса ще демонстрираме утвърдени практики при изграждане на database layer на сложни системи в 
                                C# приложения чрез вградени имплементации на шаблона Repository и използването на слой на услугите (Service Layer), както и импорт и експорт към различни формати за данни (JSON, XML).",
                StartDate = new DateTime(2021, 4, 17),
                EndDate = new DateTime(2021, 8, 25),
                IsApproved = true,
                Lecturers = new List<CourseLecturer>
                {
                    new CourseLecturer
                    {
                        LecturerId = dbContext.Lecturers.FirstOrDefault().Id,
                    },
                },
                Price = 200,
                Subject = dbContext.Subjects.FirstOrDefault(s => s.Name == "Informatics"),
                File = new File
                {
                    Extension = "docx",
                    RemoteUrl = "https://res.cloudinary.com/dm2yls09j/image/upload/v1618066729/courses-files/k5xy0emqwjdu4nn7jduf.jpg",
                },
                CreatorId = dbContext.Users.FirstOrDefault(u => u.UserName == "niki").Id,
            };

            dbContext.Courses.Add(course1);
            dbContext.Courses.Add(course2);
            await dbContext.SaveChangesAsync();
        }
    }
}
