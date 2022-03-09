namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task CreateAsync(CreateReviewInputModel input)
        {
            Review review = new Review
            {
                Content = input.Content,
                UserId = input.UserId,
                CourseId = input.CourseId,
                Rating = input.Rating,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByCourseId<T>(int id)
        {
            return this.reviewsRepository
                .All()
                .OrderByDescending(r => r.CreatedOn)
                .Where(r => r.CourseId == id)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetTop3Recent<T>()
        {
            return this.reviewsRepository
                .All()
                .OrderByDescending(r => r.Rating)
                .ThenByDescending(r => r.CreatedOn)
                .Take(3)
                .To<T>()
                .ToList();
        }
    }
}
