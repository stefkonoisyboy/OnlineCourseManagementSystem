namespace OnlineCourseManagementSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Moq;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data.Tests.Common;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
    using Xunit;

    public class LecturersServiceTests
    {
        public LecturersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<Lecturer> testData = this.GetLecturersTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Lecturer>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ILecturersService lecturersService = new LecturersService(mockedRepository.Object, null);
            IEnumerable<string> expectedIds = testData
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.Id)
                .ToList();
            IEnumerable<DateTime> expectedUsersCreatedOn = testData
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.CreatedOn)
                .ToList();
            IEnumerable<string> expectedProfileImageUrls = testData
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.ProfileImageUrl)
                .ToList();
            IEnumerable<string> expectedFirstNames = testData
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.FirstName)
                .ToList();
            IEnumerable<string> expectedLastNames = testData
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.LastName)
                .ToList();
            IEnumerable<string> expectedEmails = testData
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.Email)
                .ToList();
            IEnumerable<string> expectedUserNames = testData
                .Where(l => l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.UserName)
                .ToList();

            // Act
            IEnumerable<AllLecturersViewModel> actual = lecturersService.GetAll<AllLecturersViewModel>();

            // Arrange
            Assert.Equal(expectedProfileImageUrls, actual.Select(l => l.UserProfileImageUrl));
            Assert.Equal(expectedFirstNames, actual.Select(l => l.UserFirstName));
            Assert.Equal(expectedLastNames, actual.Select(l => l.UserLastName));
            Assert.Equal(expectedEmails, actual.Select(l => l.UserEmail));
            Assert.Equal(expectedIds, actual.Select(l => l.Id));
            Assert.Equal(expectedUserNames, actual.Select(l => l.UserUserName));
            Assert.Equal(expectedUsersCreatedOn, actual.Select(l => l.UserCreatedOn));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByCourseId_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<CourseLecturer> testData = this.GetCourseLecturersTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<CourseLecturer>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ILecturersService lecturersService = new LecturersService(null, mockedRepository.Object);
            IEnumerable<string> expectedProfileImageurls = testData
                            .Where(cl => cl.CourseId == courseId && cl.Lecturer.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                            .OrderBy(cl => cl.Lecturer.User.FirstName + ' ' + cl.Lecturer.User.LastName)
                            .ThenBy(cl => cl.Lecturer.User.UserName)
                            .Select(cl => cl.Lecturer.User.ProfileImageUrl)
                            .ToList();
            IEnumerable<string> expectedFirstNames = testData
                            .Where(cl => cl.CourseId == courseId && cl.Lecturer.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                            .OrderBy(cl => cl.Lecturer.User.FirstName + ' ' + cl.Lecturer.User.LastName)
                            .ThenBy(cl => cl.Lecturer.User.UserName)
                            .Select(cl => cl.Lecturer.User.FirstName)
                            .ToList();
            IEnumerable<string> expectedLastNames = testData
                            .Where(cl => cl.CourseId == courseId && cl.Lecturer.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                            .OrderBy(cl => cl.Lecturer.User.FirstName + ' ' + cl.Lecturer.User.LastName)
                            .ThenBy(cl => cl.Lecturer.User.UserName)
                            .Select(cl => cl.Lecturer.User.LastName)
                            .ToList();
            IEnumerable<string> expectedBackgrounds = testData.Where(cl => cl.CourseId == courseId && cl.Lecturer.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                            .OrderBy(cl => cl.Lecturer.User.FirstName + ' ' + cl.Lecturer.User.LastName)
                            .ThenBy(cl => cl.Lecturer.User.UserName)
                            .Select(cl => cl.Lecturer.User.Background)
                            .ToList();

            // Act
            IEnumerable<AllLecturersByIdViewModel> actual = lecturersService.GetAllByCourseId<AllLecturersByIdViewModel>(courseId);

            // Arrange
            Assert.Equal(expectedProfileImageurls, actual.Select(cl => cl.LecturerUserProfileImageUrl));
            Assert.Equal(expectedFirstNames, actual.Select(cl => cl.LecturerUserFirstName));
            Assert.Equal(expectedLastNames, actual.Select(cl => cl.LecturerUserLastName));
            Assert.Equal(expectedBackgrounds, actual.Select(cl => cl.LecturerUserBackground));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllById_Should_Work_Corre(int courseId)
        {
            // Arrange
            List<Lecturer> testData = this.GetLecturersTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Lecturer>>();
            mockedRepository.Setup(l => l.All()).Returns(testData.AsQueryable());
            ILecturersService lecturersService = new LecturersService(mockedRepository.Object, null);
            IEnumerable<string> expectedIds = testData
                .Where(l => l.Courses.Any(c => c.Id == courseId) && l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.Id)
                .ToList();
            IEnumerable<DateTime> expectedUsersCreatedOn = testData
                .Where(l => l.Courses.Any(c => c.Id == courseId) && l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.CreatedOn)
                .ToList();
            IEnumerable<string> expectedProfileImageUrls = testData
                .Where(l => l.Courses.Any(c => c.Id == courseId) && l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.ProfileImageUrl)
                .ToList();
            IEnumerable<string> expectedFirstNames = testData
                 .Where(l => l.Courses.Any(c => c.Id == courseId) && l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                 .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                 .ThenBy(l => l.User.UserName)
                 .Select(l => l.User.FirstName)
                 .ToList();
            IEnumerable<string> expectedLastNames = testData
                 .Where(l => l.Courses.Any(c => c.Id == courseId) && l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                 .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                 .ThenBy(l => l.User.UserName)
                 .Select(l => l.User.LastName)
                 .ToList();
            IEnumerable<string> expectedUserNames = testData
                .Where(l => l.Courses.Any(c => c.Id == courseId) && l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.UserName)
                .ToList();
            IEnumerable<string> expectedEmails = testData
                .Where(l => l.Courses.Any(c => c.Id == courseId) && l.User.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderBy(l => l.User.FirstName + ' ' + l.User.LastName)
                .ThenBy(l => l.User.UserName)
                .Select(l => l.User.Email)
                .ToList();

            // Act
            IEnumerable<AllLecturersViewModel> actual = lecturersService.GetAllById<AllLecturersViewModel>(courseId);

            // Arrange
            Assert.Equal(expectedProfileImageUrls, actual.Select(l => l.UserProfileImageUrl));
            Assert.Equal(expectedFirstNames, actual.Select(l => l.UserLastName));
            Assert.Equal(expectedLastNames, actual.Select(l => l.UserLastName));
            Assert.Equal(expectedEmails, actual.Select(l => l.UserEmail));
            Assert.Equal(expectedIds, actual.Select(l => l.Id));
            Assert.Equal(expectedUserNames, actual.Select(l => l.UserUserName));
            Assert.Equal(expectedUsersCreatedOn, actual.Select(l => l.UserCreatedOn));
        }

        private List<Lecturer> GetLecturersTestData()
        {
            List<Lecturer> students = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = "1",
                    User = new ApplicationUser
                    {
                        Id = "1",
                        FirstName = "John",
                        LastName = "Smith",
                        UserName = "johnsmith123",
                        ProfileImageUrl = "url1",
                        Roles = new HashSet<IdentityUserRole<string>>
                        {
                            new IdentityUserRole<string>
                            {
                                RoleId = "1Lecturer",
                            },
                        },
                    },
                    Courses = new List<CourseLecturer>
                    {
                        new CourseLecturer
                        {
                            CourseId = 1,
                            LecturerId = "1",
                        },
                    },
                },
                new Lecturer
                {
                    Id = "2",
                    User = new ApplicationUser
                    {
                        Id = "2",
                        FirstName = "Gosho",
                        LastName = "Petkov",
                        UserName = "gosho123",
                        ProfileImageUrl = "url2",
                        Roles = new HashSet<IdentityUserRole<string>>
                        {
                            new IdentityUserRole<string>
                            {
                                RoleId = "2Lecturer",
                            },
                        },
                    },
                    Courses = new List<CourseLecturer>
                    {
                        new CourseLecturer
                        {
                            CourseId = 2,
                            LecturerId = "2",
                        },
                    },
                },
            };

            return students;
        }

        private List<CourseLecturer> GetCourseLecturersTestData()
        {
            List<CourseLecturer> courseLecturers = new List<CourseLecturer>
            {
                new CourseLecturer
                {
                    CourseId = 1,
                    Lecturer = new Lecturer
                    {
                        Id = "1",
                        User = new ApplicationUser
                        {
                             Id = "1",
                             FirstName = "John",
                             LastName = "Smith",
                             UserName = "johnsmith123",
                             ProfileImageUrl = "url1",
                             Roles = new HashSet<IdentityUserRole<string>>
                             {
                                 new IdentityUserRole<string>
                                 {
                                     RoleId = "1Lecturer",
                                 },
                             },
                        },
                    },
                },
                new CourseLecturer
                {
                    CourseId = 1,
                    Lecturer = new Lecturer
                    {
                        Id = "2",
                        User = new ApplicationUser
                        {
                            Id = "2",
                            FirstName = "Gosho",
                            LastName = "Petkov",
                            UserName = "gosho123",
                            ProfileImageUrl = "url2",
                            Roles = new HashSet<IdentityUserRole<string>>
                            {
                                new IdentityUserRole<string>
                                {
                                    RoleId = "2Lecturer",
                                },
                            },
                        },
                    },
                },
            };

            return courseLecturers;
        }
    }
}
