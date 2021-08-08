using OnlineCourseManagementSystem.Data.Common.Repositories;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
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
    }
}
