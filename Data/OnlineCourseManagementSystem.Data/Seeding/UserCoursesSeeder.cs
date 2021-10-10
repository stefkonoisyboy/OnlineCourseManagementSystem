namespace OnlineCourseManagementSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;

    public class UserCoursesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UserCourses.Any())
            {
                return;
            }

            ICollection<UserCourse> userCourses = new List<UserCourse>()
            {
                new UserCourse
                {
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "dimi03").Id,
                },
                new UserCourse
                {
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "angata").Id,
                },
            };

            await dbContext.UserCourses.AddRangeAsync(userCourses);
            await dbContext.SaveChangesAsync();
        }
    }
}
