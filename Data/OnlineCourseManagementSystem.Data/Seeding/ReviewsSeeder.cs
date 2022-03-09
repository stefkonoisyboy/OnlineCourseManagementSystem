namespace OnlineCourseManagementSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;

    public class ReviewsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Reviews.Any())
            {
                return;
            }

            ICollection<Review> reviews = new List<Review>()
            {
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "dimi03").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 3,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "angata").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 4,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "dimi03").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 5,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "angata").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "dimi03").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "angata").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "dimi03").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "angata").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "dimi03").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "angata").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "dimi03").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "dimi03").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "angata").Id,
                    CourseId = dbContext.Courses.FirstOrDefault().Id,
                    Rating = 2,
                },
            };

            await dbContext.AddRangeAsync(reviews);
            await dbContext.SaveChangesAsync();
        }
    }
}
