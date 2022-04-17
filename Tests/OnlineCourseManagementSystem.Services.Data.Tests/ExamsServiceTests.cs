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
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;
    using Xunit;

    public class ExamsServiceTests
    {
        public ExamsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1)]
        public void GetNameById_Should_Work_Correctly(int id)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            string expectedName = testData.FirstOrDefault(x => x.Id == id).Name;

            // Act
            string actual = examsService.GetNameById(id);

            // Assert
            Assert.Equal(expectedName, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetCourseNameById_Should_Work_Correctly(int id)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            string expectedCourseName = testData.FirstOrDefault(x => x.Id == id).Course.Name;

            // Act
            string actual = examsService.GetCourseNameById(id);

            // Assert
            Assert.Equal(expectedCourseName, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetExamIdByQuestionId_Should_Work_Correctly(int questionId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            int expectedId = testData.FirstOrDefault(x => x.Questions.Any(q => q.Id == questionId)).Id;

            // Act
            int actual = examsService.GetExamIdByQuestionId(questionId);

            // Assert
            Assert.Equal(expectedId, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetDurationById_Should_Work_Correctly(int id)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            int expectedDuration = testData.FirstOrDefault(x => x.Id == id).Duration;

            // Act
            int actual = examsService.GetDurationById(id);

            // Assert
            Assert.Equal(expectedDuration, actual);
        }

        [Theory]
        [InlineData("1", "some")]
        public void GetExamsCountByUserId_Should_Work_Correctly(string userId, string searchItem)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            int expectedCount = testData.Count(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId) && (e.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Description.ToLower().Contains(searchItem.ToLower()))); ;

            // Act
            int actual = examsService.GetExamsCountByUserId(userId, searchItem);

            // Assert
            Assert.Equal(expectedCount, actual);
        }

        [Theory]
        [InlineData("1", "some")]
        public void GetResultsCountByUserId_Should_Work_Correctly(string userId, string searchItem)
        {
            // Arrange
            List<UserExam> testData = this.GetUserExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserExam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, mockedRepository.Object, null, null, null, null, null);
            int expectedCount = testData.Count(ue => ue.UserId == userId && (ue.Exam.Course.Name.ToLower().Contains(searchItem.ToLower()) || ue.Exam.Name.Contains(searchItem.ToLower())));

            // Act
            int actual = examsService.GetResultsCountByUserId(userId, searchItem);

            // Assert
            Assert.Equal(expectedCount, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        public void GetPointsByUserIdAndExamId_Should_Work_Correctly(string userId, int examId)
        {
            // Arrange
            List<Answer> testData = this.GetAnswersTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Answer>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, mockedRepository.Object, null, null, null, null, null, null, null);
            int expectedPoints = testData.Where(a => a.Question.ExamId == examId && a.UserId == userId).Sum(a => a.Question.Points);

            // Act
            int actual = examsService.GetPointsByUserIdAndExamId(userId, examId);

            // Assert
            Assert.Equal(expectedPoints, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetCourseIdByExamId_Should_Work_Correcly(int examId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            int expectedCourseId = testData.FirstOrDefault(x => x.Id == examId).CourseId.Value;

            // Act
            int actual = examsService.GetCourseIdByExam(examId);

            // Assert
            Assert.Equal(expectedCourseId, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetCertificatedExamByCourseId_Should_Work_Correcly(int courseId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            int expectedId = testData.FirstOrDefault(e => e.CourseId == courseId && e.IsCertificated.Value).Id;

            // Act
            int actual = examsService.GetCertificatedExamIdByCourse(courseId);

            // Assert
            Assert.Equal(expectedId, actual);
        }

        [Theory]
        [InlineData(1, 6)]
        public void GetCountOfUsersWithLowerGradesOnCertainExam_Should_Work_Correctly(int examId, double grade)
        {
            // Arrange
            List<UserExam> testData = this.GetUserExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserExam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, mockedRepository.Object, null, null, null, null, null);
            double expectedCount = testData.Count(ue => ue.ExamId == examId && ue.Grade < grade);

            // Act
            double actual = examsService.GetCountOfUsersWithLowerGradesOnCertainExam(examId, grade);

            // Assert
            Assert.Equal(expectedCount, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        public void GetGradeByUserIdAndCourseId_Should_Work_Correctly(string userId, int courseId)
        {
            // Arrange
            List<UserExam> testData = this.GetUserExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserExam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, mockedRepository.Object, null, null, null, null, null);
            double grade = testData.FirstOrDefault(ue => ue.UserId == userId && ue.Exam.CourseId == courseId).Grade;

            // Act
            double actual = examsService.GetGradeByUserIdAndCourseId(userId, courseId);

            // Assert
            Assert.Equal(grade, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        public void GetExamIdByUserIdAndCourseId_Should_Work_Correctly(string userId, int courseId)
        {
            // Arrange
            List<UserExam> testData = this.GetUserExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserExam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, mockedRepository.Object, null, null, null, null, null);
            int expectedExamId = testData.FirstOrDefault(ue => ue.UserId == userId && ue.Exam.CourseId == courseId).ExamId;

            // Act
            int actual = examsService.GetExamIdByUserIdAndCourseId(userId, courseId);

            // Assert
            Assert.Equal(expectedExamId, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetCountOfAllUsersWhoPassedCertainExam_Should_Work_Correctly(int examId)
        {
            // Arrange
            List<UserExam> testData = this.GetUserExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserExam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, mockedRepository.Object, null, null, null, null, null);
            int expectedCount = testData.Count(ue => ue.ExamId == examId);

            // Act
            int actual = examsService.GetCountOfAllUsersWhoPassedCertainExam(examId);

            // Assert
            Assert.Equal(expectedCount, actual);
        }

        [Theory]
        [InlineData(1, 1)]
        public void IsExamAddedToLecture_Should_Work_Correctly(int examId, int lectureId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            bool expected = true;

            // Act
            bool actual = examsService.IsExamAddedToLecture(examId, lectureId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "1")]
        public void HasUserMadeCertainExam_Should_Work_Correctly(int examId, string userId)
        {
            // Arrange
            List<UserExam> testData = this.GetUserExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserExam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, mockedRepository.Object, null, null, null, null, null);
            bool expected = true;

            // Act
            bool actual = examsService.HasUserMadeCertainExam(examId, userId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void IsExamCertificated_Should_Work_Correctly(int examId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            bool expected = true;

            // Act
            bool actual = examsService.IsExamCertificated(examId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "1")]
        public void CanStartCertificate_Should_Work_Correctly(int courseId, string userId)
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            List<Completition> completitionsTestData = this.GetCompletitionsTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            var mockedCompletitionsRepository = new Mock<IDeletableEntityRepository<Completition>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            mockedCompletitionsRepository.Setup(x => x.All()).Returns(completitionsTestData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, null, mockedLecturesRepository.Object, null, mockedCompletitionsRepository.Object, null, null);
            bool expected = true;

            // Act
            bool actual = examsService.CanStartCertificate(courseId, userId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void IsExamActive_Should_Work_Correctly(int examId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            bool expected = false;

            // Act
            bool actual = examsService.IsExamActive(examId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetStartDateById_Should_Work_Correctly(int examId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            DateTime expectedStartDate = testData.FirstOrDefault(x => x.Id == examId).StartDate;

            // Act
            DateTime actual = examsService.GetStartDateById(examId);

            // Assert
            Assert.Equal(expectedStartDate, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int examId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            int expectedId = testData.FirstOrDefault(x => x.Id == examId).Id;
            string expectedName = testData.FirstOrDefault(x => x.Id == examId).Name;
            string expectedCourseName = testData.FirstOrDefault(x => x.Id == examId).Course.Name;
            string expectedLecturerFirstName = testData.FirstOrDefault(x => x.Id == examId).Lecturer.FirstName;
            string expectedLecturerLastName = testData.FirstOrDefault(x => x.Id == examId).Lecturer.LastName;
            DateTime expectedStartDate = testData.FirstOrDefault(x => x.Id == examId).StartDate;
            DateTime expectedEndDate = testData.FirstOrDefault(x => x.Id == examId).EndDate;

            // Act
            AllExamsViewModel actual = examsService.GetById<AllExamsViewModel>(examId);

            // Assert
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedName, actual.Name);
            Assert.Equal(expectedCourseName, actual.CourseName);
            Assert.Equal(expectedLecturerFirstName, actual.LecturerFirstName);
            Assert.Equal(expectedLecturerLastName, actual.LecturerLastName);
            Assert.Equal(expectedStartDate, actual.StartDate);
            Assert.Equal(expectedEndDate, actual.EndDate);
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Name).ToList();
            IEnumerable<string> expectedCourseNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Course.Name).ToList();
            IEnumerable<string> expectedLecturerFirstNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Lecturer.FirstName).ToList();
            IEnumerable<string> expectedLecturerLastNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Lecturer.LastName).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.EndDate).ToList();

            // Act
            IEnumerable<AllExamsViewModel> actual = examsService.GetAll<AllExamsViewModel>(1, "test");

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedCourseNames, actual.Select(x => x.CourseName));
            Assert.Equal(expectedLecturerFirstNames, actual.Select(x => x.LecturerFirstName));
            Assert.Equal(expectedLecturerLastNames, actual.Select(x => x.LecturerLastName));
            Assert.Equal(expectedStartDates, actual.Select(x => x.StartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.EndDate));
        }

        [Theory]
        [InlineData(1, "1", "some")]
        public void GetAllByUserId_Should_Work_Correctly(int page, string userId, string searchItem)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Skip((page - 1) * 5).Take(5).Where(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId) && (e.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Description.ToLower().Contains(searchItem.ToLower()))).OrderByDescending(x => x.StartDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Skip((page - 1) * 5).Take(5).Where(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId) && (e.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Description.ToLower().Contains(searchItem.ToLower()))).OrderByDescending(x => x.StartDate).Select(x => x.Name).ToList();
            IEnumerable<string> expectedCourseNames = testData.Skip((page - 1) * 5).Take(5).Where(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId) && (e.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Description.ToLower().Contains(searchItem.ToLower()))).OrderByDescending(x => x.StartDate).Select(x => x.Course.Name).ToList();
            IEnumerable<string> expectedLecturerFirstNames = testData.Skip((page - 1) * 5).Take(5).Where(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId) && (e.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Description.ToLower().Contains(searchItem.ToLower()))).OrderByDescending(x => x.StartDate).Select(x => x.Lecturer.FirstName).ToList();
            IEnumerable<string> expectedLecturerLastNames = testData.Skip((page - 1) * 5).Take(5).Where(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId) && (e.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Description.ToLower().Contains(searchItem.ToLower()))).OrderByDescending(x => x.StartDate).Select(x => x.Lecturer.LastName).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Skip((page - 1) * 5).Take(5).Where(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId) && (e.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Description.ToLower().Contains(searchItem.ToLower()))).OrderByDescending(x => x.StartDate).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Skip((page - 1) * 5).Take(5).Where(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId) && (e.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Name.ToLower().Contains(searchItem.ToLower()) || e.Course.Description.ToLower().Contains(searchItem.ToLower()))).OrderByDescending(x => x.StartDate).Select(x => x.EndDate).ToList();

            // Act
            IEnumerable<AllExamsViewModel> actual = examsService.GetAllByUserId<AllExamsViewModel>(page, userId, searchItem);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedCourseNames, actual.Select(x => x.CourseName));
            Assert.Equal(expectedLecturerFirstNames, actual.Select(x => x.LecturerFirstName));
            Assert.Equal(expectedLecturerLastNames, actual.Select(x => x.LecturerLastName));
            Assert.Equal(expectedStartDates, actual.Select(x => x.StartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.EndDate));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByLectureId_Should_Work_Correctly(int lectureId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(e => e.LectureId == lectureId).OrderByDescending(x => x.CreatedOn).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Where(e => e.LectureId == lectureId).OrderByDescending(x => x.CreatedOn).Select(x => x.Name).ToList();
            IEnumerable<string> expectedCourseNames = testData.Where(e => e.LectureId == lectureId).OrderByDescending(x => x.CreatedOn).Select(x => x.Course.Name).ToList();
            IEnumerable<string> expectedLecturerFirstNames = testData.Where(e => e.LectureId == lectureId).OrderByDescending(x => x.CreatedOn).Select(x => x.Lecturer.FirstName).ToList();
            IEnumerable<string> expectedLecturerLastNames = testData.Where(e => e.LectureId == lectureId).OrderByDescending(x => x.CreatedOn).Select(x => x.Lecturer.LastName).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.Where(e => e.LectureId == lectureId).OrderByDescending(x => x.CreatedOn).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.Where(e => e.LectureId == lectureId).OrderByDescending(x => x.CreatedOn).Select(x => x.EndDate).ToList();

            // Act
            IEnumerable<AllExamsViewModel> actual = examsService.GetAllByLectureId<AllExamsViewModel>(lectureId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedCourseNames, actual.Select(x => x.CourseName));
            Assert.Equal(expectedLecturerFirstNames, actual.Select(x => x.LecturerFirstName));
            Assert.Equal(expectedLecturerLastNames, actual.Select(x => x.LecturerLastName));
            Assert.Equal(expectedStartDates, actual.Select(x => x.StartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.EndDate));
        }

        [Fact]
        public void GetAllByAdmin_Should_Work_Correctly()
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<int> expectedIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Name).ToList();
            IEnumerable<string> expectedCourseNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Course.Name).ToList();
            IEnumerable<string> expectedLecturerFirstNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Lecturer.FirstName).ToList();
            IEnumerable<string> expectedLecturerLastNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Lecturer.LastName).ToList();
            IEnumerable<DateTime> expectedStartDates = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.StartDate).ToList();
            IEnumerable<DateTime> expectedEndDates = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.EndDate).ToList();

            // Act
            IEnumerable<AllExamsViewModel> actual = examsService.GetAllByAdmin<AllExamsViewModel>();

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedCourseNames, actual.Select(x => x.CourseName));
            Assert.Equal(expectedLecturerFirstNames, actual.Select(x => x.LecturerFirstName));
            Assert.Equal(expectedLecturerLastNames, actual.Select(x => x.LecturerLastName));
            Assert.Equal(expectedStartDates, actual.Select(x => x.StartDate));
            Assert.Equal(expectedEndDates, actual.Select(x => x.EndDate));
        }

        [Theory]
        [InlineData("miro")]
        public void GetAllExamsAsSelectListItemsByCreatorId_Should_Work_Correctly(string creatorId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            IEnumerable<string> expectedTexts = testData.Where(x => x.CreatorId == creatorId).Select(x => x.Name).ToList();
            IEnumerable<string> expectedValues = testData.Where(x => x.CreatorId == creatorId).Select(x => x.Id.ToString()).ToList();

            // Act
            IEnumerable<SelectListItem> actual = examsService.GetAllExamsAsSelectListItemsByCreatorId(creatorId);

            // Assert
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
            Assert.Equal(expectedValues, actual.Select(x => x.Value));
        }

        [Theory]
        [InlineData(1, 1)]
        public void CheckAlreadyUsedExam_Should_Work_Correctly(int examId, int courseId)
        {
            // Arrange
            List<Exam> testData = this.GetExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Exam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(mockedRepository.Object, null, null, null, null, null, null, null, null);
            bool expected = true;

            // Act
            bool actual = examsService.CheckAlreadyUsedExam(examId, courseId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        public void GetByExamIdAndUserId_Should_Work_Correctly(string userId, int examId)
        {
            // Arrange
            List<UserExam> testData = this.GetUserExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserExam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, mockedRepository.Object, null, null, null, null, null);
            string expectedFirstName = testData.FirstOrDefault(x => x.UserId == userId && x.ExamId == examId).User.FirstName;
            string expectedLastName = testData.FirstOrDefault(x => x.UserId == userId && x.ExamId == examId).User.LastName;

            // Act
            ResultFromExamViewModel actual = examsService.GetByExamIdAndUserId<ResultFromExamViewModel>(userId, examId);

            // Assert
            Assert.Equal(expectedFirstName, actual.UserFirstName);
            Assert.Equal(expectedLastName, actual.UserLastName);
        }

        [Theory]
        [InlineData(1, "1", "some")]
        public void GetAllByCurrentUserId_Should_Work_Correctly(int page, string userId, string searchItem)
        {
            // Arrange
            List<UserExam> testData = this.GetUserExamsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserExam>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IExamsService examsService = new ExamsService(null, null, null, mockedRepository.Object, null, null, null, null, null);
            IEnumerable<string> expectedFirstNames = testData.Skip((page - 1) * 5).Take(5).OrderByDescending(ue => ue.CreatedOn).Where(ue => ue.UserId == userId && (ue.Exam.Course.Name.ToLower().Contains(searchItem.ToLower()) || ue.Exam.Name.Contains(searchItem.ToLower()))).Select(x => x.User.FirstName).ToList();
            IEnumerable<string> expectedLastNames = testData.Skip((page - 1) * 5).Take(5).OrderByDescending(ue => ue.CreatedOn).Where(ue => ue.UserId == userId && (ue.Exam.Course.Name.ToLower().Contains(searchItem.ToLower()) || ue.Exam.Name.Contains(searchItem.ToLower()))).Select(x => x.User.LastName).ToList();

            // Act
            IEnumerable<ResultFromExamViewModel> actual = examsService.GetAllByCurrentUserId<ResultFromExamViewModel>(page, userId, searchItem);

            // Assert
            Assert.Equal(expectedFirstNames, actual.Select(x => x.UserFirstName));
            Assert.Equal(expectedLastNames, actual.Select(x => x.UserLastName));
        }

        private List<Exam> GetExamsTestData()
        {
            List<Exam> exams = new List<Exam>()
            {
                new Exam
                {
                    Id = 1,
                    Name = "Final exam",
                    Description = "Some description",
                    Course = new Course
                    {
                        Id = 1,
                        Name = "C# Web",
                        Users = new List<UserCourse>()
                        {
                            new UserCourse
                            {
                                UserId = "1",
                                CourseId = 1,
                            },
                        },
                    },
                    Lecturer = new ApplicationUser
                    {
                        Id = "3",
                        FirstName = "Timo",
                        LastName = "Werner",
                    },
                    LecturerId = "3",
                    CourseId = 1,
                    LectureId = 1,
                    IsCertificated = true,
                    Questions = new List<Question>()
                    {
                        new Question
                        {
                            Id = 1,
                        },
                        new Question
                        {
                            Id = 2,
                        },
                    },
                    Duration = 10,
                    Users = new List<UserExam>()
                    {
                        new UserExam
                        {
                            ExamId = 1,
                            UserId = "1",
                        },
                    },
                    CreatorId = "miro",
                },
                new Exam
                {
                    Id = 2,
                    Name = "Retake exam",
                    Description = "C# basics description",
                    IsCertificated = false,
                    Course = new Course
                    {
                        Id = 2,
                        Name = "C# Advanced",
                        Users = new List<UserCourse>()
                        {
                            new UserCourse
                            {
                                UserId = "2",
                                CourseId = 2,
                            },
                        },
                    },
                    Lecturer = new ApplicationUser
                    {
                        Id = "4",
                        FirstName = "Timo",
                        LastName = "Werner",
                    },
                    LecturerId = "4",
                    CourseId = 2,
                    LectureId = 2,
                    Questions = new List<Question>()
                    {
                        new Question
                        {
                            Id = 3,
                        },
                        new Question
                        {
                            Id = 4,
                        },
                    },
                    Duration = 10,
                    Users = new List<UserExam>()
                    {
                        new UserExam
                        {
                            ExamId = 2,
                            UserId = "2",
                        },
                    },
                    CreatorId = "stefko",
                },
            };

            return exams;
        }

        private List<UserExam> GetUserExamsTestData()
        {
            List<UserExam> userExams = new List<UserExam>()
            {
                new UserExam
                {
                    Exam = new Exam
                    {
                        Id = 1,
                        Name = "Final exam",
                        Description = "Some description",
                        Course = new Course
                        {
                            Id = 1,
                            Name = "C# Web",
                            Users = new List<UserCourse>()
                            {
                                new UserCourse
                                {
                                    UserId = "1",
                                    CourseId = 1,
                                },
                            },
                        },
                        CourseId = 1,
                    },
                    ExamId = 1,
                    User = new ApplicationUser
                    {
                        Id = "1",
                        FirstName = "Timo",
                        LastName = "Werner",
                    },
                    UserId = "1",
                    Grade = 5,
                },
                new UserExam
                {
                    Exam = new Exam
                    {
                        Id = 2,
                        Name = "Final exam",
                        Description = "Some description",
                        Course = new Course
                        {
                            Id = 2,
                            Name = "C# Web",
                            Users = new List<UserCourse>()
                            {
                                new UserCourse
                                {
                                    UserId = "2",
                                    CourseId = 2,
                                },
                            },
                        },
                        CourseId = 2,
                    },
                    ExamId = 2,
                    User = new ApplicationUser
                    {
                        Id = "2",
                        FirstName = "Jorginho",
                        LastName = "Frello",
                    },
                    UserId = "2",
                    Grade = 4,
                },
            };

            return userExams;
        }

        private List<Answer> GetAnswersTestData()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer
                {
                    Question = new Question
                    {
                        Id = 1,
                        ExamId = 1,
                        Points = 2,
                    },
                    QuestionId = 1,
                    UserId = "1",
                },
                new Answer
                {
                    Question = new Question
                    {
                        Id = 2,
                        ExamId = 1,
                        Points = 3,
                    },
                    QuestionId = 2,
                    UserId = "1",
                },
            };

            return answers;
        }

        private List<Lecture> GetLecturesTestData()
        {
            List<Lecture> lectures = new List<Lecture>()
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 1,
                },
            };

            return lectures;
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
