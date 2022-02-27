namespace OnlineCourseManagementSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data.Tests.Common;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;
    using Xunit;

    public class CoursesServiceTests
    {
        public CoursesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void Create_Should_Work_Correctly()
        {
            List<Course> courses = new List<Course>();
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
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
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
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
            List<Completition> completitionsTestData = this.GetCompletitionsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            var mockedCompletitionsRepository = new Mock<IDeletableEntityRepository<Completition>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            mockedCompletitionsRepository.Setup(x => x.All()).Returns(completitionsTestData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, mockedCompletitionsRepository.Object, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(x => x.Users.Any(u => u.UserId == userId) && x.Lectures.Count() > 0 && x.Lectures.Count() == completitionsTestData.Count(comp => comp.UserId == userId && comp.Lecture.CourseId == x.Id)).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllCompletedByUserId<AllCoursesViewModel>(userId);

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
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
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

        [Fact]
        public void GetAllByNameOrTag_Should_Work_Correctly()
        {
            // Arrange
            SearchByCourseNameOrTagInputModel input = new SearchByCourseNameOrTagInputModel
            {
                Text = "Web",
            };

            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text))).OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllByNameOrTag<AllCoursesViewModel>(input);

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
        public void GetAllRecommended_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.OrderByDescending(x => x.Reviews.Average(r => r.Rating)).ThenByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllRecommended<AllCoursesViewModel>();

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
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int id)
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            int expectedId = testData.FirstOrDefault(x => x.Id == id).Id;
            string expectedName = testData.FirstOrDefault(x => x.Id == id).Name;
            string expectedDescription = testData.FirstOrDefault(x => x.Id == id).Description;
            decimal expectedPrice = testData.FirstOrDefault(x => x.Id == id).Price;
            DateTime expectedStartDate = testData.FirstOrDefault(x => x.Id == id).StartDate;
            DateTime expectedEndDate = testData.FirstOrDefault(x => x.Id == id).EndDate;
            string expectedFileRemoteUrl = testData.FirstOrDefault(x => x.Id == id).File.RemoteUrl;
            string expectedSubjectName = testData.FirstOrDefault(x => x.Id == id).Subject.Name;
            int expectedUsersCount = testData.FirstOrDefault(x => x.Id == id).Users.Count();

            // Act
            AllCoursesViewModel actual = coursesService.GetById<AllCoursesViewModel>(id);

            // Assert
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedName, actual.Name);
            Assert.Equal(expectedDescription, actual.Description);
            Assert.Equal(expectedPrice, actual.Price);
            Assert.Equal(expectedStartDate, actual.StartDate);
            Assert.Equal(expectedEndDate, actual.EndDate);
            Assert.Equal(expectedFileRemoteUrl, actual.FileRemoteUrl);
            Assert.Equal(expectedSubjectName, actual.SubjectName);
            Assert.Equal(expectedUsersCount, actual.Users.Count());
        }

        [Theory]
        [InlineData(1, "1")]
        public void GetAllByUser_Should_Work_Correctly(int id, string userId)
        {
            // Arrange
            List<UserCourse> testData = this.GetUserCoursesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserCourse>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(null, null, null, null, mockedRepository.Object, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.Id);
            IEnumerable<string> expectedNames = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.Name);
            IEnumerable<string> expectedDescriptions = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.Description);
            IEnumerable<decimal> expectedPrices = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.Price);
            IEnumerable<DateTime> expectedStartDates = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.StartDate);
            IEnumerable<DateTime> expectedEndDates = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.EndDate);
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.File.RemoteUrl);
            IEnumerable<string> expectedSubjectNames = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.Subject.Name);
            IEnumerable<int> expectedCourseLecturesCounts = testData.Where(x => x.UserId == userId).OrderByDescending(x => x.Course.StartDate).Skip((id - 1) * 6).Take(6).Select(x => x.Course.Lectures.Count());

            // Act
            IEnumerable<AllCoursesByUserViewModel> actual = coursesService.GetAllByUser<AllCoursesByUserViewModel>(id, userId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.CourseId));
            Assert.Equal(expectedNames, actual.Select(x => x.CourseName));
            Assert.Equal(expectedDescriptions, actual.Select(x => x.CourseDescription));
            Assert.Equal(expectedPrices, actual.Select(x => x.CoursePrice));
            Assert.Equal(expectedStartDates, actual.Select(x => x.CourseStartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.CourseEndDate));
            Assert.Equal(expectedFileRemoteUrls, actual.Select(x => x.CourseFileRemoteUrl));
            Assert.Equal(expectedSubjectNames, actual.Select(x => x.CourseSubjectName));
        }

        [Theory]
        [InlineData(1, "3")]
        public void GetAllByCreatorId_Should_Work_Correctly(int id, string creatorId)
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(x => x.CreatorId == creatorId).Skip((id - 1) * 6).Take(6).OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllByCreatorId<AllCoursesViewModel>(id, creatorId);

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
        public void GetAllUpcoming_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(x => x.StartDate > DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllUpcoming<AllCoursesViewModel>();

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
        public void GetAllPast_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllPast<AllCoursesViewModel>();

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
        [InlineData(1, "name")]
        public void GetAllActive_Should_Work_Correctly(int id, string name)
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(x => x.StartDate < DateTime.UtcNow && (x.Name.Contains(name) || x.Tags.Any(t => t.Tag.Name.Contains(name)))).Skip((id - 1) * 5).Take(5).OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllActive<AllCoursesViewModel>(id, name);

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
        public void GetAllByTag_Should_Work_Correctly()
        {
            // Arrange
            SearchByTagInputModel input = new SearchByTagInputModel
            {
                Name = "Web",
            };

            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.Where(c => c.Tags.Any(t => t.Tag.Name.Contains(input.Name))).OrderByDescending(x => x.StartDate).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllByTag<AllCoursesViewModel>(input);

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
        public void GetAllActiveAsSelectListItems_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<string> expectedTexts = testData.Where(x => x.StartDate >= DateTime.UtcNow && x.EndDate <= DateTime.UtcNow).Select(x => x.Name);
            IEnumerable<string> expectedValues = testData.Where(x => x.StartDate >= DateTime.UtcNow && x.EndDate <= DateTime.UtcNow).Select(x => x.Id.ToString());

            // Act
            IEnumerable<SelectListItem> actual = coursesService.GetAllActive();

            // Assert
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
            Assert.Equal(expectedValues, actual.Select(x => x.Value));
        }

        [Fact]
        public void GetAllAsSelectListItems_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<string> expectedTexts = testData.OrderBy(x => x.Name).Select(x => x.Name);
            IEnumerable<string> expectedValues = testData.OrderBy(x => x.Name).Select(x => x.Id.ToString());

            // Act
            IEnumerable<SelectListItem> actual = coursesService.GetAllAsSelectListItems();

            // Assert
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
            Assert.Equal(expectedValues, actual.Select(x => x.Value));
        }

        [Theory]
        [InlineData("3")]
        public void GetAllAsSelectListItemsByCreatorId_Should_Work_Correctly(string creatorId)
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<string> expectedTexts = testData.Where(x => x.CreatorId == creatorId).OrderBy(x => x.Name).Select(x => x.Name);
            IEnumerable<string> expectedValues = testData.Where(x => x.CreatorId == creatorId).OrderBy(x => x.Name).Select(x => x.Id.ToString());

            // Act
            IEnumerable<SelectListItem> actual = coursesService.GetAllAsSelectListItemsByCreatorId(creatorId);

            // Assert
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
            Assert.Equal(expectedValues, actual.Select(x => x.Value));
        }

        [Fact]
        public void GetAllByAdmin_Should_Work_Correctly()
        {
            // Arrange
            List<Course> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Course>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICoursesService coursesService = new CoursesService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Name).ToList();
            IEnumerable<string> expectedDescriptions = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Description).ToList();
            IEnumerable<decimal> expectedPrices = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Price).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.EndDate).ToList();
            IEnumerable<string> expectedFileRemoteUrls = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.File.RemoteUrl).ToList();
            IEnumerable<string> expectedSubjectNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Subject.Name).ToList();
            IEnumerable<int> expectedUsersCounts = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Users.Count()).ToList();

            // Act
            IEnumerable<AllCoursesViewModel> actual = coursesService.GetAllByAdmin<AllCoursesViewModel>();

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
                    CreatorId = "3",
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
                        },
                    },
                    Tags = new List<CourseTag>()
                    {
                        new CourseTag
                        {
                            CourseId = 1,
                            Tag = new Tag
                            {
                                Id = 1,
                                Name = "Web",
                            },
                            TagId = 1,
                        },
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Rating = 5,
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
                    CreatorId = "4",
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
                            UserId = "2",
                            CourseId = 2,
                        },
                    },
                    Lectures = new List<Lecture>()
                    {
                        new Lecture
                        {
                            Id = 2,
                        },
                    },
                    Tags = new List<CourseTag>()
                    {
                        new CourseTag
                        {
                            CourseId = 2,
                            Tag = new Tag
                            {
                                Id = 2,
                                Name = "C#",
                            },
                        },
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Rating = 5,
                        },
                    },
                },
            };

            return courses;
        }

        private List<Completition> GetCompletitionsTestData()
        {
            List<Completition> completitions = new List<Completition>()
            {
                new Completition
                {
                    Id = 1,
                    UserId = "1",
                    Lecture = new Lecture
                    {
                        CourseId = 1,
                    },
                },
            };

            return completitions;
        }

        private List<UserCourse> GetUserCoursesTestData()
        {
            List<UserCourse> userCourses = new List<UserCourse>()
            {
                new UserCourse
                {
                    UserId = "1",
                    Course = new Course
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
                        Lectures = new List<Lecture>()
                        {
                            new Lecture
                            {
                                Id = 1,
                            },
                        },
                    },
                    CourseId = 1,
                },
            };

            return userCourses;
        }
    }
}
