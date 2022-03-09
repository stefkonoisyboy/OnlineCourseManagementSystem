namespace OnlineCourseManagementSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data.Tests.Common;
    using OnlineCourseManagementSystem.Web.ViewModels.Orders;
    using Xunit;

    public class OrdersServiceTests
    {
        public OrdersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1")]
        public void GetAllByUserId_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<Order> testData = this.GetOrdersTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Order>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IOrdersService ordersService = new OrdersService(mockedRepository.Object);
            IEnumerable<int> expectedCourseIds = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).Select(x => x.CourseId).ToList();
            IEnumerable<string> expectedCourseFileRemoteUrls = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).Select(x => x.Course.File.RemoteUrl).ToList();
            IEnumerable<string> expectedCourseNames = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).Select(x => x.Course.Name).ToList();
            IEnumerable<string> expectedCourseDescriptions = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).Select(x => x.Course.Description).ToList();
            IEnumerable<decimal> expectedCoursePrices = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).Select(x => x.Course.Price).ToList();
            IEnumerable<DateTime> expectedCourseStartDates = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).Select(x => x.Course.StartDate).ToList();
            IEnumerable<DateTime> expectedCourseEndDates = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).Select(x => x.Course.EndDate).ToList();

            // Act
            IEnumerable<AllOrdersByUserIdViewModel> actual = ordersService.GetAllByUserId<AllOrdersByUserIdViewModel>(userId);

            // Assert
            Assert.Equal(expectedCourseIds, actual.Select(x => x.CourseId));
            Assert.Equal(expectedCourseFileRemoteUrls, actual.Select(x => x.CourseFileRemoteUrl));
            Assert.Equal(expectedCourseNames, actual.Select(x => x.CourseName));
            Assert.Equal(expectedCourseDescriptions, actual.Select(x => x.CourseDescription));
            Assert.Equal(expectedCoursePrices, actual.Select(x => x.CoursePrice));
            Assert.Equal(expectedCourseStartDates, actual.Select(x => x.CourseStartDate));
            Assert.Equal(expectedCourseEndDates, actual.Select(x => x.CourseEndDate));
        }

        [Theory]
        [InlineData("1")]
        public void CoursesInCartCount_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<Order> testData = this.GetOrdersTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Order>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IOrdersService ordersService = new OrdersService(mockedRepository.Object);
            int expectedCount = testData.Count(x => x.UserId == userId);

            // Act
            int actual = ordersService.CoursesInCartCount(userId);

            // Assert
            Assert.Equal(expectedCount, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        public void IsOrderAvailable_Should_Work_Correctly(string userId, int courseId)
        {
            // Arrange
            List<Order> testData = this.GetOrdersTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Order>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IOrdersService ordersService = new OrdersService(mockedRepository.Object);
            bool expected = !testData.Any(x => x.UserId == userId && x.CourseId == courseId);

            // Act
            bool actual = ordersService.IsOrderAvailable(courseId, userId);

            // Assert
            Assert.Equal(expected, actual);
        }

        private List<Order> GetOrdersTestData()
        {
            List<Order> orders = new List<Order>()
            {
                new Order
                {
                    Id = 1,
                    UserId = "1",
                    Course = new Course
                    {
                        Id = 1,
                        Name = "C# Web",
                        Description = "Some description",
                        Price = 20,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow,
                        File = new File
                        {
                            Id = 1,
                            RemoteUrl = "remote",
                        },
                        FileId = 1,
                    },
                    CourseId = 1,
                    CreatedOn = DateTime.UtcNow,
                },
                new Order
                {
                    Id = 2,
                    UserId = "2",
                    Course = new Course
                    {
                        Id = 2,
                        Name = "C# Advanced",
                        Description = "Some description",
                        Price = 20,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow,
                        File = new File
                        {
                            Id = 2,
                            RemoteUrl = "remote",
                        },
                        FileId = 2,
                    },
                    CourseId = 2,
                    CreatedOn = DateTime.UtcNow,
                },
            };

            return orders;
        }
    }
}
