namespace OnlineCourseManagementSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            ApplicationUser admin1 = new ApplicationUser
            {
                Id = "1",
                Email = "stefko_coniovski@abv.bg",
                UserName = "stefkonoisyboy",
                FirstName = "Stefko",
                LastName = "Tsonyovski",
                PhoneNumber = "0876148608",
                Background = "Some background",
                BirthDate = new DateTime(2003, 7, 2),
                Gender = Gender.Male,
                TownId = dbContext.Towns.FirstOrDefault(t => t.Name == "Smolyan").Id,
                Address = "Some Address",
                Title = Title.Mr,
            };

            ApplicationUser admin2 = new ApplicationUser
            {
                Id = "2",
                Email = "miroslav.uzunov23@gmail.com",
                UserName = "Ghostmaster",
                FirstName = "Miroslav",
                LastName = "Uzunov",
                PhoneNumber = "0876148608",
                Background = "Some background",
                BirthDate = new DateTime(2003, 12, 25),
                Gender = Gender.Male,
                TownId = dbContext.Towns.FirstOrDefault(t => t.Name == "Smolyan").Id,
                Address = "Some Address",
                Title = Title.Mr,
            };

            ApplicationUser student1 = new ApplicationUser
            {
                Id = "3",
                Email = "dim_petrov@abv.bg",
                UserName = "dimi03",
                FirstName = "Dimitar",
                LastName = "Petrov",
                PhoneNumber = "0876148608",
                BirthDate = new DateTime(2003, 7, 2),
                Gender = Gender.Male,
                TownId = dbContext.Towns.FirstOrDefault(t => t.Name == "Plovdiv").Id,
                StudentId = "student1",
                Address = "Some Address",
                Title = Title.Mr,
            };

            Student studentUser1 = new Student
            {
                Id = "student1",
                ParentId = "parent1",
            };

            ApplicationUser student2 = new ApplicationUser
            {
                Id = "4",
                Email = "angel_dimov@abv.bg",
                UserName = "angata",
                FirstName = "Angel",
                LastName = "Dimov",
                PhoneNumber = "0876148608",
                BirthDate = new DateTime(2003, 7, 2),
                Gender = Gender.Male,
                TownId = dbContext.Towns.FirstOrDefault(t => t.Name == "Varna").Id,
                StudentId = "student2",
                Address = "Some Address",
                Title = Title.Mr,
            };

            Student studentUser2 = new Student
            {
                Id = "student2",
                ParentId = "parent2",
            };

            ApplicationUser parent1 = new ApplicationUser
            {
                Id = "5",
                Email = "ivan_ivanov@abv.bg",
                UserName = "vankata",
                FirstName = "Ivan",
                LastName = "Ivanov",
                PhoneNumber = "0876148608",
                BirthDate = new DateTime(1980, 7, 2),
                Gender = Gender.Male,
                TownId = dbContext.Towns.FirstOrDefault(t => t.Name == "Plovdiv").Id,
                ParentId = "parent1",
                Address = "Some Address",
                Title = Title.Mr,
            };

            Parent parentUser1 = new Parent
            {
                Id = "parent1",
            };

            ApplicationUser parent2 = new ApplicationUser
            {
                Id = "6",
                Email = "george70@abv.bg",
                UserName = "george70",
                FirstName = "Georgi",
                LastName = "Georgiev",
                PhoneNumber = "0876148608",
                BirthDate = new DateTime(2003, 7, 2),
                Gender = Gender.Male,
                TownId = dbContext.Towns.FirstOrDefault(t => t.Name == "Varna").Id,
                ParentId = "parent2",
                Address = "Some Address",
                Title = Title.Mr,
            };

            Parent parentUser2 = new Parent
            {
                Id = "parent2",
            };

            ApplicationUser lecturer1 = new ApplicationUser
            {
                Id = "7",
                Email = "admin_nikolay@abv.bg",
                UserName = "niki",
                FirstName = "Nikolay",
                LastName = "Kostov",
                PhoneNumber = "0876148608",
                Background = "Some background",
                BirthDate = new DateTime(1990, 7, 2),
                Gender = Gender.Male,
                TownId = dbContext.Towns.FirstOrDefault(t => t.Name == "Sofia").Id,
                LecturerId = "lecturer1",
                Address = "Some Address",
                Title = Title.Dr,
            };

            Lecturer lecturerUser1 = new Lecturer
            {
                Id = "lecturer1",
            };

            ApplicationUser lecturer2 = new ApplicationUser
            {
                Id = "8",
                Email = "stoyan_shopov@abv.bg",
                UserName = "stoikata",
                FirstName = "Stoyan",
                LastName = "Shopov",
                PhoneNumber = "0876148608",
                Background = "Some background",
                BirthDate = new DateTime(2003, 7, 2),
                Gender = Gender.Male,
                TownId = dbContext.Towns.FirstOrDefault(t => t.Name == "Sofia").Id,
                LecturerId = "lecturer2",
                Address = "Some Address",
                Title = Title.Dr,
            };

            Lecturer lecturerUser2 = new Lecturer
            {
                Id = "lecturer2",
            };

            await dbContext.Parents.AddAsync(parentUser1);
            await dbContext.Parents.AddAsync(parentUser2);
            await dbContext.Students.AddAsync(studentUser1);
            await dbContext.Students.AddAsync(studentUser2);
            await dbContext.Lecturers.AddAsync(lecturerUser1);
            await dbContext.Lecturers.AddAsync(lecturerUser2);

            await userManager.CreateAsync(admin1, "Mirostef123!");
            await userManager.CreateAsync(admin2, "Mirostef123!");
            await userManager.CreateAsync(student1, "Mirostef123!");
            await userManager.CreateAsync(student2, "Mirostef123!");
            await userManager.CreateAsync(parent1, "Mirostef123!");
            await userManager.CreateAsync(parent2, "Mirostef123!");
            await userManager.CreateAsync(lecturer1, "Mirostef123!");
            await userManager.CreateAsync(lecturer2, "Mirostef123!");

            await userManager.AddToRoleAsync(admin1, GlobalConstants.AdministratorRoleName);
            await userManager.AddToRoleAsync(admin2, GlobalConstants.AdministratorRoleName);
            await userManager.AddToRoleAsync(student1, GlobalConstants.StudentRoleName);
            await userManager.AddToRoleAsync(student2, GlobalConstants.StudentRoleName);
            await userManager.AddToRoleAsync(parent1, GlobalConstants.ParentRoleName);
            await userManager.AddToRoleAsync(parent2, GlobalConstants.ParentRoleName);
            await userManager.AddToRoleAsync(lecturer1, GlobalConstants.LecturerRoleName);
            await userManager.AddToRoleAsync(lecturer2, GlobalConstants.LecturerRoleName);

            await dbContext.SaveChangesAsync();
        }
    }
}
