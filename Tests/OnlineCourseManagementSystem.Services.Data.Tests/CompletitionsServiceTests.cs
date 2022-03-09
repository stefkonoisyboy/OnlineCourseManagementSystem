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
    using Xunit;

    public class CompletitionsServiceTests
    {
        [Theory]
        [InlineData(1, "1")]
        public void GetAllCompletitionsCountByCourseIdAndUserId_Should_Work_Correctly(int courseId, string userId)
        {
            // Arrange
            List<Completition> testData = this.GetCompletitionsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Completition>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICompletitionsService completitionsService = new CompletitionsService(mockedRepository.Object);
            int expectedCount = testData.Count(c => c.Lecture.CourseId == courseId && c.UserId == userId);

            // Act
            int actual = completitionsService.GetAllCompletitionsCountByCourseIdAndUserId(courseId, userId);

            // Assert
            Assert.Equal(expectedCount, actual);
        }

        private List<Completition> GetCompletitionsTestData()
        {
            List<Completition> completitions = new List<Completition>()
            {
                new Completition
                {
                    UserId = "1",
                    Lecture = new Lecture
                    {
                        Id = 1,
                        CourseId = 1,
                    },
                    LectureId = 1,
                },
                new Completition
                {
                    UserId = "1",
                    Lecture = new Lecture
                    {
                        Id = 2,
                        CourseId = 1,
                    },
                    LectureId = 2,
                },
            };

            return completitions;
        }
    }
}
