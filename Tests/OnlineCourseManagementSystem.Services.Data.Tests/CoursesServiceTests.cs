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
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using Xunit;

    public class CoursesServiceTests
    {
        public CoursesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAll<AllCoursesViewModel>();

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedDescriptions, actual.Select(x => x.Description));
            Assert.Equal(expectedPrices, actual.Select(x => x.Price));
            Assert.Equal(expectedStartDates, actual.Select(x => x.StartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.EndDate));
            Assert.Equal(expectedFileRemoteUrls, actual.Select(x => x.FileRemoteUrl));
            Assert.Equal(expectedSubjectNames, actual.Select(x => x.SubjectName));
            Assert.Equal(expectedUsersCounts, actual.Select(x => x.Users.Count()));
        }

        [Fact]
        public void GetAllUnapproved_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllUnapproved<AllCoursesViewModel>();

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedDescriptions, actual.Select(x => x.Description));
            Assert.Equal(expectedPrices, actual.Select(x => x.Price));
            Assert.Equal(expectedStartDates, actual.Select(x => x.StartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.EndDate));
            Assert.Equal(expectedFileRemoteUrls, actual.Select(x => x.FileRemoteUrl));
            Assert.Equal(expectedSubjectNames, actual.Select(x => x.SubjectName));
            Assert.Equal(expectedUsersCounts, actual.Select(x => x.Users.Count()));
        }

        [Theory]
        [InlineData("1")]
        public void GetAllCompletedByUserId_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == x.Lectures.FirstOrDefault().Completitions.Count()).OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllUnapproved<AllCoursesViewModel>();

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedDescriptions, actual.Select(x => x.Description));
            Assert.Equal(expectedPrices, actual.Select(x => x.Price));
            Assert.Equal(expectedStartDates, actual.Select(x => x.StartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.EndDate));
            Assert.Equal(expectedFileRemoteUrls, actual.Select(x => x.FileRemoteUrl));
            Assert.Equal(expectedSubjectNames, actual.Select(x => x.SubjectName));
            Assert.Equal(expectedUsersCounts, actual.Select(x => x.Users.Count()));
        }

        [Theory]
        [InlineData("1")]
        public void GetAllFollowedByUserId_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(x => x.Users.Any(u => u.UserId == userId)).OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllFollewedByUserId<AllCoursesViewModel>(userId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedDescriptions, actual.Select(x => x.Description));
            Assert.Equal(expectedPrices, actual.Select(x => x.Price));
            Assert.Equal(expectedStartDates, actual.Select(x => x.StartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.EndDate));
            Assert.Equal(expectedFileRemoteUrls, actual.Select(x => x.FileRemoteUrl));
            Assert.Equal(expectedSubjectNames, actual.Select(x => x.SubjectName));
            Assert.Equal(expectedUsersCounts, actual.Select(x => x.Users.Count()));
        }

        private List<Course> GetTestData()
        {
            List<Course> courses = new List<Course>()
            {
                new Course
                {
                    Id = 1,
                    Name = "C# Basics",
                    Description = "Some description",
                    Price = 20,
                    StartDate = new DateTime(1, 1, 1),
                    EndDate = new DateTime(1, 1, 1),
                    File = new File
                    {
                        Id = 1,
                        RemoteUrl = "remote",
                    },
                    FileId = 1,
                    Subject = new Subject
                    {
                        Id = 1,
                        Name = "I.T.",
                    },
                    SubjectId = 1,
                    Users = new List<UserCourse>()
                    {
                        new UserCourse
                        {
                            UserId = "1",
                            CourseId = 1,
                        },
                    },
                    Lectures = new List<Lecture>()
                    {
                        new Lecture
                        {
                            Id = 1,
                            Completitions = new List<Completition>()
                            {
                                new Completition
                                {
                                    Id = 1,
                                    LectureId = 1,
                                    UserId = "1",
                                },
                            },
                        },
                    },
                },
                new Course
                {
                    Id = 2,
                    Name = "C# Basics",
                    Description = "Some description",
                    Price = 20,
                    StartDate = new DateTime(1, 1, 1),
                    EndDate = new DateTime(1, 1, 1),
                    File = new File
                    {
                        Id = 2,
                        RemoteUrl = "remote",
                    },
                    FileId = 2,
                    Subject = new Subject
                    {
                        Id = 2,
                        Name = "I.T.",
                    },
                    SubjectId = 2,
                    Users = new List<UserCourse>()
                    {
                        new UserCourse
                        {
                            UserId = "1",
                            CourseId = 2,
                        },
                    },
                    Lectures = new List<Lecture>()
                    {
                        new Lecture
                        {
                            Id = 2,
                            Completitions = new List<Completition>()
                            {
                                new Completition
                                {
                                    Id = 2,
                                    LectureId = 2,
                                    UserId = "1",
                                },
                            },
                        },
                    },
                },
            };

            return courses;
        }
    }
}
