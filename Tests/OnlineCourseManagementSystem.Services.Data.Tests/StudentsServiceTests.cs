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
    using OnlineCourseManagementSystem.Web.ViewModels.Students;
    using Xunit;

    public class StudentsServiceTests
    {
        public StudentsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1", "2")]
        public async Task AddParent_Should_Work_Correctly(string studentId, string parentId)
        {
            // Arrange
            List<Student> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Student>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IStudentsService studentsService = new StudentsService(mockedRepository.Object);
            AddParentInputModel inputModel = new AddParentInputModel
            {
                StudentId = studentId,
                ParentId = parentId,
            };
            Student student = testData.FirstOrDefault(x => x.Id == studentId);

            // Act
            await studentsService.AddParent(inputModel);

            // Arrange
            Assert.Equal(student.ParentId, inputModel.ParentId);
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<Student> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Student>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IStudentsService studentsService = new StudentsService(mockedRepository.Object);
            IEnumerable<string> expectedUserFirstNames = testData
                .Where(s => s.User.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .OrderBy(s => s.User.FirstName + ' ' + s.User.LastName)
                .ThenBy(s => s.User.UserName)
                .Select(s => s.User.FirstName)
                .ToList();
            IEnumerable<string> expectedUserLastNames = testData
                .Where(s => s.User.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .OrderBy(s => s.User.FirstName + ' ' + s.User.LastName)
                .ThenBy(s => s.User.UserName)
                .Select(s => s.User.LastName)
                .ToList();

            // Act
            IEnumerable<AllStudentsByIdViewModel> actual = studentsService.GetAll<AllStudentsByIdViewModel>();

            // Arrange
            Assert.Equal(expectedUserFirstNames, actual.Select(s => s.UserFirstName));
            Assert.Equal(expectedUserLastNames, actual.Select(s => s.UserLastName));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByCourse_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<Student> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Student>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IStudentsService studentsService = new StudentsService(mockedRepository.Object);
            IEnumerable<string> expectedFirstNames = testData
                .Where(s => s.User.Courses.Any(c => c.CourseId == courseId) && s.User.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .OrderBy(s => s.User.FirstName + ' ' + s.User.LastName)
                .ThenBy(s => s.User.UserName)
                .Select(s => s.User.FirstName)
                .ToList();
            IEnumerable<string> expectedLastNames = testData
              .Where(s => s.User.Courses.Any(c => c.CourseId == courseId) && s.User.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
              .OrderBy(s => s.User.FirstName + ' ' + s.User.LastName)
              .ThenBy(s => s.User.UserName)
              .Select(s => s.User.LastName)
              .ToList();

            // Act
            IEnumerable<AllStudentsByIdViewModel> actual = studentsService.GetAllByCourse<AllStudentsByIdViewModel>(courseId);

            // Arrange
            Assert.Equal(expectedFirstNames, actual.Select(s => s.UserFirstName));
            Assert.Equal(expectedLastNames, actual.Select(s => s.UserLastName));
        }

        [Theory]
        [InlineData("1")]
        public void GetAllById_Should_Work_Correctly(string parentId)
        {
            // Arrange
            List<Student> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Student>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IStudentsService studentsService = new StudentsService(mockedRepository.Object);
            IEnumerable<string> expectedFirstNames = testData
                .Where(s => s.ParentId == parentId)
                .OrderBy(s => s.User.FirstName + ' ' + s.User.LastName)
                .ThenBy(s => s.User.UserName)
                .Select(s => s.User.FirstName)
                .ToList();
            IEnumerable<string> expectedLastNames = testData
               .Where(s => s.ParentId == parentId)
               .OrderBy(s => s.User.FirstName + ' ' + s.User.LastName)
               .ThenBy(s => s.User.UserName)
               .Select(s => s.User.LastName)
               .ToList();

            // Act
            IEnumerable<AllStudentsByIdViewModel> actual = studentsService.GetAllById<AllStudentsByIdViewModel>(parentId);

            // Arrange
            Assert.Equal(expectedFirstNames, actual.Select(s => s.UserFirstName));
            Assert.Equal(expectedLastNames, actual.Select(s => s.UserLastName));
        }

        [Theory]
        [InlineData("1")]
        public void GetFullNameShouldWorkCorrectly(string studentId)
        {
            // Arrange
            List<Student> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Student>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IStudentsService studentsService = new StudentsService(mockedRepository.Object);
            string expectedFullName = testData
                .Where(s => s.Id == studentId && s.User.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .Select(s => s.User.FirstName + " " + s.User.LastName)
                .FirstOrDefault();

            // Act
            string actual = studentsService.GetFullNameById(studentId);

            // Arrange
            Assert.Equal(expectedFullName, actual);
        }

        private List<Student> GetTestData()
        {
            List<Student> students = new List<Student>()
            {
                new Student
                {
                    Id = "1",
                    ParentId = null,
                    User = new ApplicationUser
                    {
                        Id = "1",
                        FirstName = "Gosho",
                        LastName = "Petkov",
                        UserName = "goshopetkov123",
                        Roles = new HashSet<IdentityUserRole<string>>
                        {
                            new IdentityUserRole<string>
                            {
                                RoleId = "1Student",
                            },
                        },
                    },
                },
                new Student
                {
                    Id = "2",
                    ParentId = "1",
                    User = new ApplicationUser
                    {
                        Id = "2",
                        FirstName = "John",
                        LastName = "Smith",
                        UserName = "johnsmith123",
                        Roles = new HashSet<IdentityUserRole<string>>
                        {
                            new IdentityUserRole<string>
                            {
                                RoleId = "2Student",
                            },
                        },
                    },
                },
            };

            return students;
        }
    }
}
