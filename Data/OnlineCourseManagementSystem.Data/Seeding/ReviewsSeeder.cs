using OnlineCourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Data.Seeding
{
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
                    UserId = "3",
                    CourseId = 8,
                    Rating = 3,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 8,
                    Rating = 4,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "3",
                    CourseId = 8,
                    Rating = 5,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 8,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 3,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 4,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 5,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 6,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 7,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 9,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 17,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 18,
                    Rating = 2,
                },
                new Review
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A accusamus aut consectetur consequatur cum cupiditate debitis doloribus, error ex explicabo harum illum minima mollitia nisi",
                    UserId = "4",
                    CourseId = 19,
                    Rating = 2,
                },
            };

            await dbContext.AddRangeAsync(reviews);
            await dbContext.SaveChangesAsync();
        }
    }
}
