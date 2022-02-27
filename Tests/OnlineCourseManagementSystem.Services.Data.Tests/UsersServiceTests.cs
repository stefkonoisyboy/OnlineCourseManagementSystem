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
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Data.Tests.Common;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using Xunit;

    public class UsersServiceTests
    {
        public UsersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            IEnumerable<string> expectedBackgrounds = testData.Select(x => x.Background).ToList();
            IEnumerable<string> expectedFirstNames = testData.Select(x => x.FirstName).ToList();
            IEnumerable<string> expectedLastNames = testData.Select(x => x.LastName).ToList();
            IEnumerable<string> expectedProfileImageUrls = testData.Select(x => x.ProfileImageUrl).ToList();

            // Act
            IEnumerable<UserViewModel> actual = usersService.GetAll<UserViewModel>();

            // Assert
            Assert.Equal(expectedBackgrounds, actual.Select(x => x.Background));
            Assert.Equal(expectedFirstNames, actual.Select(x => x.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(x => x.LastName));
            Assert.Equal(expectedProfileImageUrls, actual.Select(x => x.ProfileImageUrl));
        }

        [Theory]
        [InlineData("1")]
        public void GetById_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            string expectedProfileImageUrl = testData.FirstOrDefault(x => x.Id == userId).ProfileImageUrl;
            string expectedFirstName = testData.FirstOrDefault(x => x.Id == userId).FirstName;
            string expectedLastName = testData.FirstOrDefault(x => x.Id == userId).LastName;
            string expectedBackground = testData.FirstOrDefault(x => x.Id == userId).Background;

            // Act
            UserViewModel actual = usersService.GetById<UserViewModel>(userId);

            // Assert
            Assert.Equal(expectedProfileImageUrl, actual.ProfileImageUrl);
            Assert.Equal(expectedFirstName, actual.FirstName);
            Assert.Equal(expectedLastName, actual.LastName);
            Assert.Equal(expectedBackground, actual.Background);
        }

        [Fact]
        public void GetAllLecturers_Should_Work_Correctly()
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            IEnumerable<string> expectedIds = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.Id).ToList();
            IEnumerable<string> expectedFirstNames = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.FirstName).ToList();
            IEnumerable<string> expectedLastNames = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.LastName).ToList();
            IEnumerable<string> expectedBackgrounds = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.Background).ToList();
            IEnumerable<string> expectedProfileImageUrls = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.ProfileImageUrl).ToList();
            int expectedLecturersCount = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).Count();

            // Act
            IEnumerable<UserViewModel> actual = usersService.GetAllLecturers<UserViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedFirstNames, actual.Select(x => x.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(x => x.LastName));
            Assert.Equal(expectedBackgrounds, actual.Select(x => x.Background));
            Assert.Equal(expectedProfileImageUrls, actual.Select(x => x.ProfileImageUrl));
            Assert.Equal(expectedLecturersCount, actual.Count());
        }

        [Fact]
        public void GetAllStudents_Should_Work_Correctly()
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            IEnumerable<string> expectedIds = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.Id).ToList();
            IEnumerable<string> expectedFirstNames = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.FirstName).ToList();
            IEnumerable<string> expectedLastNames = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.LastName).ToList();
            IEnumerable<string> expectedBackgrounds = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.Background).ToList();
            IEnumerable<string> expectedProfileImageUrls = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(c => c.ProfileImageUrl).ToList();
            int expectedLecturersCount = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student")).Count();

            // Act
            IEnumerable<UserViewModel> actual = usersService.GetAllStudents<UserViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedFirstNames, actual.Select(x => x.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(x => x.LastName));
            Assert.Equal(expectedBackgrounds, actual.Select(x => x.Background));
            Assert.Equal(expectedProfileImageUrls, actual.Select(x => x.ProfileImageUrl));
            Assert.Equal(expectedLecturersCount, actual.Count());
        }

        [Theory]
        [InlineData("1")]
        public void GetFullNameById_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            string expectedFullName = testData.Where(x => x.Id == userId).Select(x => $"{x.FirstName} {x.LastName}").FirstOrDefault();

            // Act
            string actual = usersService.GetFullNameById(userId);

            // Arrange
            Assert.Equal(expectedFullName, actual);
        }

        [Theory]
        [InlineData("1")]
        public void GetProfileImageUrlById_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            string expectedProfileImageUrl = testData.FirstOrDefault(x => x.Id == userId).ProfileImageUrl;

            // Act
            string actual = usersService.GetProfileImageUrlById(userId);

            // Arrange
            Assert.Equal(expectedProfileImageUrl, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetTop3ByCourseId_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            IEnumerable<string> expectedIds = testData
                                            .Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student") && x.Courses.Any(c => c.CourseId == courseId))
                                            .OrderByDescending(x => x.Assignments.Count(a => a.Assignment.CourseId == courseId))
                                            .Take(3)
                                            .Select(x => x.Id).ToList();
            IEnumerable<string> expectedFirstNames = testData
                                            .Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student") && x.Courses.Any(c => c.CourseId == courseId))
                                            .OrderByDescending(x => x.Assignments.Count(a => a.Assignment.CourseId == courseId))
                                            .Take(3)
                                            .Select(x => x.FirstName).ToList();

            IEnumerable<string> expectedLastNames = testData
                                            .Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student") && x.Courses.Any(c => c.CourseId == courseId))
                                            .OrderByDescending(x => x.Assignments.Count(a => a.Assignment.CourseId == courseId))
                                            .Take(3)
                                            .Select(x => x.LastName).ToList();

            IEnumerable<string> expectedBackgrounds = testData
                                            .Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student") && x.Courses.Any(c => c.CourseId == courseId))
                                            .OrderByDescending(x => x.Assignments.Count(a => a.Assignment.CourseId == courseId))
                                            .Take(3)
                                            .Select(x => x.Background).ToList();

            IEnumerable<string> expectedProfileImageUrls = testData
                                            .Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student") && x.Courses.Any(c => c.CourseId == courseId))
                                            .OrderByDescending(x => x.Assignments.Count(a => a.Assignment.CourseId == courseId))
                                            .Take(3)
                                            .Select(x => x.ProfileImageUrl).ToList();

            // Act
            IEnumerable<UserViewModel> actual = usersService.GetTop3ByCourseId<UserViewModel>(courseId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedFirstNames, actual.Select(x => x.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(x => x.LastName));
            Assert.Equal(expectedBackgrounds, actual.Select(x => x.Background));
            Assert.Equal(expectedProfileImageUrls, actual.Select(x => x.ProfileImageUrl));
        }

        [Fact]
        public void GetTop4Students_Should_Work_Correctly()
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            IEnumerable<string> expectedIds = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                                               .OrderByDescending(x => x.Exams.Average(e => e.Grade))
                                               .Take(4)
                                               .Select(x => x.Id).ToList();
            IEnumerable<string> expectedFirstNames = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                                               .OrderByDescending(x => x.Exams.Average(e => e.Grade))
                                               .Take(4)
                                               .Select(x => x.FirstName).ToList();
            IEnumerable<string> expectedLastNames = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                                               .OrderByDescending(x => x.Exams.Average(e => e.Grade))
                                               .Take(4)
                                               .Select(x => x.LastName).ToList();
            IEnumerable<string> expectedBackgrounds = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                                               .OrderByDescending(x => x.Exams.Average(e => e.Grade))
                                               .Take(4)
                                               .Select(x => x.Background).ToList();
            IEnumerable<string> expectedProfileImageUrls = testData.Where(x => x.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                                               .OrderByDescending(x => x.Exams.Average(e => e.Grade))
                                               .Take(4)
                                               .Select(x => x.ProfileImageUrl).ToList();

            // Act
            IEnumerable<UserViewModel> actual = usersService.GetTop4Students<UserViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedFirstNames, actual.Select(x => x.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(x => x.LastName));
            Assert.Equal(expectedBackgrounds, actual.Select(x => x.Background));
            Assert.Equal(expectedProfileImageUrls, actual.Select(x => x.ProfileImageUrl));
        }

        [Fact]
        public void GetTop4Teachers_Should_Work_Correctly()
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);
            IEnumerable<string> expectedIds = testData.Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).Select(x => x.Id).ToList();
            IEnumerable<string> expectedFirstNames = testData.Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).Select(x => x.FirstName).ToList();
            IEnumerable<string> expectedLastNames = testData.Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).Select(x => x.LastName).ToList();
            IEnumerable<string> expectedBackgrounds = testData.Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).Select(x => x.Background).ToList();
            IEnumerable<string> expectedProfileImageUrls = testData.Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer")).Select(x => x.ProfileImageUrl).ToList();

            // Act
            IEnumerable<UserViewModel> actual = usersService.GetTop4Teachers<UserViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedFirstNames, actual.Select(x => x.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(x => x.LastName));
            Assert.Equal(expectedBackgrounds, actual.Select(x => x.Background));
            Assert.Equal(expectedProfileImageUrls, actual.Select(x => x.ProfileImageUrl));
        }

        [Theory]
        [InlineData("1", "21/12/2003", "John", "Timsen", "Some Background1", 1, "Some address 12", "test_user1@gmail.com")]
        public async Task UpdateAsync_Should_Work_Correctly(string id, string birthdate, string firstName, string lastName, string background, int townId, string address, string email)
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            DateTime expectedBirthDate = DateTime.Parse(birthdate);
            string expectedFirstName = firstName;
            string expectedLastName = lastName;
            string expectedBackground = background;
            int expectedTownId = townId;
            string expectedAddress = address;
            string expectedEmail = email;

            ManageAccountInputModel inputModel = new ManageAccountInputModel
            {
                Id = id,
                Birthdate = expectedBirthDate,
                FirstName = firstName,
                LastName = lastName,
                Background = background,
                TownId = townId,
                Address = address,
                Email = email,
            };
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.Update(It.IsAny<ApplicationUser>())).Callback((ApplicationUser user) =>
            {
                user.BirthDate = inputModel.Birthdate;
                user.FirstName = inputModel.FirstName;
                user.LastName = inputModel.LastName;
                user.Background = inputModel.Background;
                user.TownId = inputModel.TownId;
                user.Address = inputModel.Address;
                user.Email = inputModel.Email;
            });
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());

            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);

            // Act
            await usersService.UpdateAsync(inputModel);

            // Arrange
            ApplicationUser updatedUser = testData.FirstOrDefault(x => x.Id == inputModel.Id);

            Assert.Equal(expectedBirthDate, updatedUser.BirthDate);
            Assert.Equal(expectedFirstName, updatedUser.FirstName);
            Assert.Equal(expectedLastName, updatedUser.LastName);
            Assert.Equal(expectedBackground, updatedUser.Background);
            Assert.Equal(expectedTownId, updatedUser.TownId);
            Assert.Equal(expectedAddress, updatedUser.Address);
            Assert.Equal(expectedEmail, updatedUser.Email);
        }

        [Theory]
        [InlineData("1")]
        public async Task DeleteAsync_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<ApplicationUser> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.Delete(It.IsAny<ApplicationUser>())).Callback((ApplicationUser user) => user.IsDeleted = true);
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());

            IUsersService usersService = new UsersService(mockedRepository.Object, null, null);

            // Act
            await usersService.DeleteAsync(userId);

            // Arrange
            Assert.True(testData.FirstOrDefault(u => u.Id == userId).IsDeleted);
        }

        private List<ApplicationUser> GetTestData()
        {
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = "1",
                    Email = "test_user1@abv.bg",
                    UserName = "testuser1",
                    FirstName = "Test",
                    LastName = "User1",
                    PhoneNumber = "0876148608",
                    Background = "Some background",
                    BirthDate = new DateTime(2003, 7, 2),
                    ProfileImageUrl = "url1",
                    Gender = Gender.Male,
                    TownId = 1,
                    Address = "Some Address",
                    Title = Title.Mr,
                    Roles = new HashSet<IdentityUserRole<string>>
                    {
                      new IdentityUserRole<string>
                      {
                          RoleId = "1Lecturer",
                      },
                    },
                },
                new ApplicationUser
                {
                    Id = "2",
                    Email = "test_user2@abv.bg",
                    UserName = "testuser2",
                    FirstName = "Test",
                    LastName = "User2",
                    PhoneNumber = "0876148608",
                    Background = "Some background",
                    BirthDate = new DateTime(2003, 7, 2),
                    ProfileImageUrl = "url2",
                    Gender = Gender.Male,
                    Roles = new HashSet<IdentityUserRole<string>>
                    {
                      new IdentityUserRole<string>
                      {
                          RoleId = "2Student",
                      },
                    },
                    Assignments = new HashSet<UserAssignment>
                    {
                        new UserAssignment
                        {
                            Assignment = new Assignment
                            {
                                CourseId = 1,
                            },
                        },
                    },
                    Exams = new HashSet<UserExam>
                    {
                       new UserExam
                       {
                           Grade = 4,
                       },
                       new UserExam
                       {
                           Grade = 5,
                       },
                    },
                    TownId = 1,
                    Address = "Some Address",
                    Title = Title.Mr,
                },
                new ApplicationUser
                {
                    Id = "1",
                    Email = "test_user3@abv.bg",
                    UserName = "testuser3",
                    FirstName = "Test",
                    LastName = "User3",
                    PhoneNumber = "0876148608",
                    Background = "Some background",
                    BirthDate = new DateTime(2003, 7, 2),
                    ProfileImageUrl = "url3",
                    Gender = Gender.Male,
                    TownId = 1,
                    Address = "Some Address",
                    Title = Title.Mr,
                    Roles = new HashSet<IdentityUserRole<string>>
                    {
                      new IdentityUserRole<string>
                      {
                          RoleId = "3Student",
                      },
                    },
                    Courses = new HashSet<UserCourse>
                    {
                        new UserCourse
                        {
                            CourseId = 1,
                        },
                    },
                    Assignments = new HashSet<UserAssignment>
                    {
                    },
                    Exams = new HashSet<UserExam>
                    {
                       new UserExam
                       {
                           Grade = 6,
                       },
                       new UserExam
                       {
                           Grade = 5,
                       },
                    },
                },
            };

            return users;
        }
    }
}
