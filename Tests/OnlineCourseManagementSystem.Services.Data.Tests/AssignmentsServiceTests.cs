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
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using Xunit;

    public class AssignmentsServiceTests
    {
        public AssignmentsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1)]
        public void GetAllUsersForAssignment_Should_Work_Correctly(int assignmentId)
        {
            // Arrange
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object, null, null);
            IEnumerable<string> expectedCourseNames = testData.Where(ua => ua.AssignmentId == assignmentId).Select(a => a.Assignment.Course.Name).ToList();
            IEnumerable<string> expectedTitles = testData.Where(ua => ua.AssignmentId == assignmentId).Select(a => a.Assignment.Title).ToList();

            // Act
            IEnumerable<AssignmentViewModel> actual = assignmentsService.GetAllUsersForAssignment<AssignmentViewModel>(assignmentId);

            // Arrange
            Assert.Equal(expectedCourseNames, actual.Select(ua => ua.CourseName));
            Assert.Equal(expectedTitles, actual.Select(ua => ua.Title));
        }

        [Theory]
        [InlineData(3)]
        public void GetAllByCourse_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object,  null, null);
            IEnumerable<int> expectedIds = testData.Where(ua => ua.Assignment.CourseId == courseId).Select(a => a.AssignmentId).ToList();
            IEnumerable<string> expectedCourseNames = testData.Where(ua => ua.Assignment.CourseId == courseId).Select(a => a.Assignment.Course.Name).ToList();
            IEnumerable<string> expectedTitles = testData.Where(ua => ua.Assignment.CourseId == courseId).Select(a => a.Assignment.Title).ToList();

            // Act
            IEnumerable<AssignmentViewModel> actual = assignmentsService.GetAllBy<AssignmentViewModel>(courseId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(ua => ua.AssignmentId));
            Assert.Equal(expectedTitles, actual.Select(ua => ua.Title));
            Assert.Equal(expectedCourseNames, actual.Select(ua => ua.CourseName));
        }

        [Theory]
        [InlineData(3, "1")]
        public void GetAllByCourseAndUser_Should_Work_Correctly(int courseId, string userId)
        {
            // Arrange
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object, null, null);
            IEnumerable<int> expectedIds = testData.Where(ua => ua.UserId == ua.UserId && ua.Assignment.CourseId == courseId && ua.TurnedOn == null)
                                                      .OrderByDescending(ua => ua.CreatedOn)
                                                      .Select(ua => ua.AssignmentId).ToList();
            IEnumerable<string> expectedCourseNames = testData.Where(ua => ua.UserId == ua.UserId && ua.Assignment.CourseId == courseId && ua.TurnedOn == null)
                                                      .OrderByDescending(ua => ua.CreatedOn)
                                                      .Select(ua => ua.Assignment.Course.Name).ToList();
            IEnumerable<string> expectedTitles = testData.Where(ua => ua.UserId == ua.UserId && ua.Assignment.CourseId == courseId && ua.TurnedOn == null)
                                                      .OrderByDescending(ua => ua.CreatedOn)
                                                      .Select(ua => ua.Assignment.Title).ToList();

            // Act
            IEnumerable<AssignmentViewModel> actual = assignmentsService.GetAllByCourseAndUser<AssignmentViewModel>(courseId, userId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(ua => ua.AssignmentId));
            Assert.Equal(expectedTitles, actual.Select(ua => ua.Title));
            Assert.Equal(expectedCourseNames, actual.Select(ua => ua.CourseName));
        }

        [Theory]
        [InlineData(3, "1")]
        public void GetAllFinishedByCourseAndUser(int courseId, string userId)
        {
            // Arrange
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object, null, null);
            IEnumerable<int> expectedIds = testData.Where(ua => ua.UserId == userId && ua.Assignment.CourseId == courseId && ua.IsChecked && ua.IsChecked && ua.TurnedOn != null)
                                            .Select(ua => ua.AssignmentId).ToList();
            IEnumerable<string> expectedCourseNames = testData.Where(ua => ua.UserId == userId && ua.Assignment.CourseId == courseId && ua.IsChecked && ua.IsChecked && ua.TurnedOn != null)
                                            .Select(ua => ua.Assignment.Course.Name).ToList();
            IEnumerable<string> expectedTitles = testData.Where(ua => ua.UserId == userId && ua.Assignment.CourseId == courseId && ua.IsChecked && ua.IsChecked && ua.TurnedOn != null)
                                            .Select(ua => ua.Assignment.Title).ToList();

            // Act
            IEnumerable<AssignmentViewModel> actual = assignmentsService.GetAllFinishedByCourseAndUser<AssignmentViewModel>(courseId, userId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(ua => ua.AssignmentId));
            Assert.Equal(expectedTitles, actual.Select(ua => ua.Title));
            Assert.Equal(expectedCourseNames, actual.Select(ua => ua.CourseName));
        }

        [Theory]
        [InlineData("1")]
        public void GetAllByUser_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object, null, null);
            IEnumerable<int> expectedIds = testData.Where(ua => ua.UserId == userId && ua.User.Roles
                .FirstOrDefault().RoleId
                .EndsWith("Student") && ua.TurnedOn == null)
                .OrderByDescending(ua => ua.Assignment.StartDate)
                .Select(ua => ua.AssignmentId).ToList();
            IEnumerable<string> expectedCourseNames = testData.Where(ua => ua.UserId == userId && ua.User.Roles
                .FirstOrDefault().RoleId
                .EndsWith("Student") && ua.TurnedOn == null)
                .OrderByDescending(ua => ua.Assignment.StartDate)
                .Select(ua => ua.Assignment.Course.Name).ToList();
            IEnumerable<string> expectedTitles = testData.Where(ua => ua.UserId == userId && ua.User.Roles
                .FirstOrDefault().RoleId
                .EndsWith("Student") && ua.TurnedOn == null)
                .OrderByDescending(ua => ua.Assignment.StartDate)
                .Select(ua => ua.Assignment.Title).ToList();

            // Act
            IEnumerable<AssignmentViewModel> actual = assignmentsService.GetAllBy<AssignmentViewModel>(userId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(ua => ua.AssignmentId));
            Assert.Equal(expectedTitles, actual.Select(ua => ua.Title));
            Assert.Equal(expectedCourseNames, actual.Select(ua => ua.CourseName));
        }

        [Theory]
        [InlineData("1")]
        public void GetAllFinishedBy_Should_Work_Correclty(string userId)
        {
            // Arrange
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object, null, null);
            IEnumerable<int> expectedIds = testData.Where(ua => ua.IsChecked && ua.UserId == userId)
                .OrderByDescending(ua => ua.TurnedOn)
                .Select(ua => ua.AssignmentId);

            IEnumerable<string> expectedCourseNames = testData.Where(ua => ua.IsChecked && ua.UserId == userId)
                .OrderByDescending(ua => ua.TurnedOn)
                .Select(ua => ua.Assignment.Course.Name);

            IEnumerable<string> expectedTitles = testData.Where(ua => ua.IsChecked && ua.UserId == userId)
                .OrderByDescending(ua => ua.TurnedOn)
                .Select(ua => ua.Assignment.Title);

            // Act
            IEnumerable<AssignmentViewModel> actual = assignmentsService.GetAllFinishedBy<AssignmentViewModel>(userId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(ua => ua.AssignmentId));
            Assert.Equal(expectedTitles, actual.Select(ua => ua.Title));
            Assert.Equal(expectedCourseNames, actual.Select(ua => ua.CourseName));
        }

        [Theory]
        [InlineData(1, "2")]
        public void GetById1_Should_Work_Correctly(int assignmentId, string userId)
        {
            // Arrange
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object, null, null);
            string expectedCourseName = testData.FirstOrDefault(ua => ua.AssignmentId == assignmentId && ua.UserId == userId).Assignment.Course.Name;
            string expectedTitle = testData.FirstOrDefault(ua => ua.AssignmentId == assignmentId && ua.UserId == userId).Assignment.Title;

            // Act
            AssignmentViewModel actual = assignmentsService.GetById<AssignmentViewModel>(assignmentId, userId);

            // Arrange
            Assert.Equal(expectedCourseName, actual.CourseName);
            Assert.Equal(expectedTitle, actual.Title);
        }

        [Theory]
        [InlineData(1)]
        public void GetById2_Should_Work_Correctly(int assignmentId)
        {
            // Arrange
            List<Assignment> testData = this.GetAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Assignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(mockedRepository.Object, null, null, null);
            string expectedTitle = testData.FirstOrDefault(a => a.Id == assignmentId).Title;
            string expectedCourseName = testData.FirstOrDefault(a => a.Id == assignmentId).Course.Name;

            // Act
            AssignmentByAdminViewModel actual = assignmentsService.GetById<AssignmentByAdminViewModel>(assignmentId);

            // Arrange
            Assert.Equal(expectedTitle, actual.Title);
        }

        [Theory]
        [InlineData(3)]
        public void GetAllCheckedBy_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<Assignment> testData = this.GetAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Assignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(mockedRepository.Object, null, null, null);

            IEnumerable<int> expectedIds = testData.Where(a => a.Users.Count(ua => ua.IsChecked) == a.Users.Count && a.CourseId == courseId).Select(a => a.Id).ToList();
            IEnumerable<string> expectedTitles = testData.Where(a => a.Users.Count(ua => ua.IsChecked) == a.Users.Count && a.CourseId == courseId).Select(a => a.Title).ToList();

            // Act
            IEnumerable<AssignmentByAdminViewModel> actual = assignmentsService.GetAllCheckedBy<AssignmentByAdminViewModel>(courseId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.Id));
            Assert.Equal(expectedTitles, actual.Select(a => a.Title));
        }

        [Theory]
        [InlineData(1, "1")]
        public void GetCheckedBy_Should_Work_Correctly(int assignmentId, string userId)
        {
            // Arrange
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object, null, null);

            string expectedTitle = testData.FirstOrDefault(ua => ua.AssignmentId == assignmentId && ua.UserId == userId && ua.IsChecked).Assignment.Title;
            string expectedCourseName = testData.FirstOrDefault(ua => ua.AssignmentId == assignmentId && ua.UserId == userId && ua.IsChecked).Assignment.Course.Name;

            // Act
            AssignmentViewModel actual = assignmentsService.GetCheckedBy<AssignmentViewModel>(assignmentId, userId);

            // Arrange
            Assert.Equal(expectedTitle, actual.Title);
            Assert.Equal(expectedCourseName, actual.CourseName);
        }

        [Theory]
        [InlineData(1)]
        public void GetAllCheckedUserAssignments_Should_Work_Correctly(int assignmentId)
        {
            // Arrange\
            List<UserAssignment> testData = this.GetUserAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserAssignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(null, mockedRepository.Object, null, null);

            IEnumerable<string> expectedTitles = testData.Where(ua => ua.IsChecked && ua.AssignmentId == assignmentId).Select(ua => ua.Assignment.Title).ToList();
            IEnumerable<string> expectedCourseNames = testData.Where(ua => ua.IsChecked && ua.AssignmentId == assignmentId).Select(ua => ua.Assignment.Course.Name).ToList();

            // Act
            IEnumerable<AssignmentViewModel> actual = assignmentsService.GetAllCheckedUserAssignments<AssignmentViewModel>(assignmentId);

            // Arrange
            Assert.Equal(expectedTitles, actual.Select(ua => ua.Title));
            Assert.Equal(expectedCourseNames, actual.Select(ua => ua.CourseName));
        }

        [Fact]
        public void GetAllByAdmin_Should_Work_Correctly()
        {
            // Arrange
            List<Assignment> testData = this.GetAssignmentsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Assignment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAssignmentsService assignmentsService = new AssignmentsService(mockedRepository.Object, null, null, null);

            IEnumerable<int> expectedIds = testData.OrderByDescending(a => a.CreatedOn).Select(a => a.Id);
            IEnumerable<string> expectedTitles = testData.OrderByDescending(a => a.CreatedOn).Select(a => a.Title);

            // Act
            IEnumerable<AssignmentByAdminViewModel> actual = assignmentsService.GetAllByAdmin<AssignmentByAdminViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.Id));
            Assert.Equal(expectedTitles, actual.Select(a => a.Title));
        }

        private List<Assignment> GetAssignmentsTestData()
        {
            return new List<Assignment>()
            {
                new Assignment()
                {
                    Id = 1,
                    Title = "Title1",
                    Course = new Course()
                    {
                       Id = 2,
                       Name = "ASP.NET Core",
                    },
                    CourseId = 2,
                    StartDate = new DateTime(2021, 12, 5),
                    EndDate = new DateTime(2022, 1, 3),
                    Users = new List<UserAssignment>()
                    {
                        new UserAssignment()
                        {
                            Id = 1,
                            AssignmentId = 1,
                            IsChecked = true,
                        },
                    },
                },
                new Assignment()
                {
                    Id = 2,
                    Course = new Course()
                    {
                       Id = 3,
                       Name = "React",
                    },
                    CourseId = 3,
                    StartDate = new DateTime(2021, 12, 15),
                    EndDate = new DateTime(2022, 1, 1),
                    Title = "Title2",
                    Users = new List<UserAssignment>()
                    {
                        new UserAssignment()
                        {
                            Id = 1,
                            AssignmentId = 2,
                            IsChecked = true,
                        },
                        new UserAssignment()
                        {
                            Id = 2,
                            AssignmentId = 2,
                            IsChecked = false,
                        },
                    },
                },
                new Assignment()
                {
                    Id = 3,
                    Course = new Course()
                    {
                       Id = 3,
                       Name = "React",
                    },
                    CourseId = 3,
                    StartDate = new DateTime(2021, 12, 23),
                    EndDate = new DateTime(2022, 1, 2),
                    Title = "Title3",
                    Users = new List<UserAssignment>()
                    {
                        new UserAssignment()
                        {
                            Id = 1,
                            AssignmentId = 3,
                            IsChecked = true,
                        },
                        new UserAssignment()
                        {
                            Id = 2,
                            AssignmentId = 3,
                            IsChecked = true,
                        },
                    },
                },
            };
        }

        private List<UserAssignment> GetUserAssignmentsTestData()
        {
            return new List<UserAssignment>()
            {
                new UserAssignment()
                {
                    Id = 1,
                    AssignmentId = 1,
                    Assignment = new Assignment()
                    {
                        Id = 1,
                        Title = "Title1",
                        Course = new Course()
                        {
                           Id = 2,
                           Name = "ASP.NET Core",
                        },
                        StartDate = new DateTime(2021, 12, 5),
                        EndDate = new DateTime(2022, 1, 3),
                    },
                    UserId = "1",
                    User = new ApplicationUser()
                    {
                        Id = "1",
                        Roles = new HashSet<IdentityUserRole<string>>
                        {
                            new IdentityUserRole<string>
                            {
                                RoleId = "1Student",
                            },
                        },
                    },
                    TurnedOn = new DateTime(2021, 12, 27),
                    IsChecked = true,
                },
                new UserAssignment()
                {
                    Id = 2,
                    AssignmentId = 1,
                    Assignment = new Assignment()
                    {
                        Id = 1,
                        Title = "Title1",
                        Course = new Course()
                        {
                           Id = 3,
                           Name = "React",
                        },
                        StartDate = new DateTime(2021, 12, 6),
                        EndDate = new DateTime(2022, 1, 3),
                    },
                    UserId = "2",
                    User = new ApplicationUser()
                    {
                        Id = "2",
                        Roles = new HashSet<IdentityUserRole<string>>
                        {
                            new IdentityUserRole<string>
                            {
                                RoleId = "2Student",
                            },
                        },
                    },
                    IsChecked = true,
                },
                new UserAssignment()
                {
                    Id = 3,
                    AssignmentId = 2,
                    Assignment = new Assignment()
                    {
                        Id = 2,
                        Course = new Course()
                        {
                           Id = 3,
                           Name = "React",
                        },
                        StartDate = new DateTime(2021, 12, 15),
                        EndDate = new DateTime(2022, 1, 1),
                        Title = "Title2",
                    },
                    UserId = "1",
                    User = new ApplicationUser()
                    {
                        Id = "1",
                        Roles = new HashSet<IdentityUserRole<string>>
                        {
                            new IdentityUserRole<string>
                            {
                                RoleId = "1Student",
                            },
                        },
                    },
                    IsChecked = false,
                },
                new UserAssignment()
                {
                    Id = 4,
                    AssignmentId = 3,
                    Assignment = new Assignment()
                    {
                        Id = 3,
                        Course = new Course()
                        {
                           Id = 3,
                           Name = "React",
                        },
                        StartDate = new DateTime(2021, 12, 23),
                        EndDate = new DateTime(2022, 1, 2),
                        Title = "Title3",
                    },
                    UserId = "1",
                    User = new ApplicationUser()
                    {
                        Id = "1",
                        Roles = new HashSet<IdentityUserRole<string>>
                        {
                            new IdentityUserRole<string>
                            {
                                RoleId = "1Student",
                            },
                        },
                    },
                    TurnedOn = new DateTime(2021, 12, 28),
                    IsChecked = true,
                },
            };
        }
    }
}
