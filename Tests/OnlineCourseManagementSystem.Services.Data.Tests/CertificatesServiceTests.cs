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
    using OnlineCourseManagementSystem.Web.ViewModels.Certificates;
    using Xunit;

    public class CertificatesServiceTests
    {
        public CertificatesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1")]
        public void GetAllByUserId_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<Certificate> testData = this.GetCertificatesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Certificate>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICertificatesService certificatesService = new CertificatesService(mockedRepository.Object);
            IEnumerable<int> expectedIds = testData.Where(x => x.UserId == userId).Select(x => x.Id).ToList();
            IEnumerable<string> expectedCourseNames = testData.Where(x => x.UserId == userId).Select(x => x.Course.Name).ToList();

            // Act
            IEnumerable<AllCertificatesByUserIdViewModel> actual = certificatesService.GetAllByUserId<AllCertificatesByUserIdViewModel>(userId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedCourseNames, actual.Select(x => x.CourseName));
        }

        [Theory]
        [InlineData("1", 1)]
        public void GetByUserIdAndCourseId_Should_Work_Correctly(string userId, int courseId)
        {
            // Arrange
            List<Certificate> testData = this.GetCertificatesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Certificate>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICertificatesService certificatesService = new CertificatesService(mockedRepository.Object);
            int expectedId = testData.FirstOrDefault(x => x.UserId == userId && x.CourseId == courseId).Id;
            string expectedCourseName = testData.FirstOrDefault(x => x.UserId == userId && x.CourseId == courseId).Course.Name;

            // Act
            AllCertificatesByUserIdViewModel actual = certificatesService.GetByUserIdAndCourseId<AllCertificatesByUserIdViewModel>(userId, courseId);

            // Assert
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedCourseName, actual.CourseName);
        }

        private List<Certificate> GetCertificatesTestData()
        {
            List<Certificate> certificates = new List<Certificate>()
            {
                new Certificate
                {
                    Id = 1,
                    Course = new Course
                    {
                        Id = 1,
                        Name = "C# Advanced",
                        File = new File
                        {
                            Id = 1,
                            RemoteUrl = "remote",
                        },
                        FileId = 1,
                    },
                    CourseId = 1,
                    UserId = "1",
                },
                new Certificate
                {
                    Id = 2,
                    Course = new Course
                    {
                        Id = 2,
                        Name = "C# Web",
                        File = new File
                        {
                            Id = 2,
                            RemoteUrl = "remote",
                        },
                        FileId = 2,
                    },
                    CourseId = 2,
                    UserId = "1",
                },
            };

            return certificates;
        }
    }
}
