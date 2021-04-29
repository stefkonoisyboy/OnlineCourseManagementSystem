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

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public int CoursesInCartCount(string userId)
        {
            return this.ordersRepository.All().Where(o => o.UserId == userId).Count();
        }

        public async Task CreateAsync(int courseId, string userId)
        {
            Order order = new Order
            {
                CourseId = courseId,
                UserId = userId,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int courseId, string userId)
        {
            Order order = this.ordersRepository.All().FirstOrDefault(o => o.UserId == userId && o.CourseId == courseId);
            this.ordersRepository.Delete(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId)
        {
            return this.ordersRepository
                .All()
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedOn)
                .To<T>()
                .ToList();
        }

        public bool IsOrderAvailable(int courseId, string userId)
        {
            return !this.ordersRepository.All().Any(o => o.UserId == userId && o.CourseId == courseId);
        }
    }
}
