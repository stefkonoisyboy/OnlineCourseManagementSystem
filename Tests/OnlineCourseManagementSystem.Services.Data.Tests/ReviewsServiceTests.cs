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
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
    using Xunit;

    public class ReviewsServiceTests
    {
        public ReviewsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByCourseId_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<Review> testData = this.GetReviewsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Review>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IReviewsService reviewsService = new ReviewsService(mockedRepository.Object);
            IEnumerable<string> expectedContents = testData.Where(x => x.CourseId == courseId).OrderByDescending(x => x.CreatedOn).Select(x => x.Content).ToList();
            IEnumerable<double> expectedRatings = testData.Where(x => x.CourseId == courseId).OrderByDescending(x => x.CreatedOn).Select(x => x.Rating).ToList();

            // Act
            IEnumerable<AllReviewsByCourseIdViewModel> actual = reviewsService.GetAllByCourseId<AllReviewsByCourseIdViewModel>(courseId);

            // Assert
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
            Assert.Equal(expectedRatings, actual.Select(x => x.Rating));
        }

        [Fact]
        public void GetTop3Recent_Should_Work_Correctly()
        {
            // Arrange
            List<Review> testData = this.GetReviewsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Review>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IReviewsService reviewsService = new ReviewsService(mockedRepository.Object);
            IEnumerable<string> expectedContents = testData.OrderByDescending(x => x.Rating).ThenByDescending(x => x.CreatedOn).Select(x => x.Content).ToList();
            IEnumerable<double> expectedRatings = testData.OrderByDescending(x => x.Rating).ThenByDescending(x => x.CreatedOn).Select(x => x.Rating).ToList();

            // Act
            IEnumerable<AllReviewsByCourseIdViewModel> actual = reviewsService.GetTop3Recent<AllReviewsByCourseIdViewModel>();

            // Assert
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
            Assert.Equal(expectedRatings, actual.Select(x => x.Rating));
        }

        private List<Review> GetReviewsTestData()
        {
            List<Review> reviews = new List<Review>()
            {
                new Review
                {
                    Id = 1,
                    Content = "Richarlison",
                    Rating = 3,
                    CourseId = 1,
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Id = "stef",
                        FirstName = "Stefko",
                        LastName = "Tsonyovski",
                        ProfileImageUrl = "remote",
                    },
                    UserId = "stef",
                },
                new Review
                {
                    Id = 2,
                    Content = "Djokovic is the G. O. A. T. !",
                    Rating = 5,
                    CourseId = 1,
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Id = "miro",
                        FirstName = "Miroslav",
                        LastName = "Uzunov",
                        ProfileImageUrl = "remote",
                    },
                    UserId = "miro",
                },
            };

            return reviews;
        }
    }
}
