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
    using Xunit;

    public class SubjectsServiceTests
    {
        [Fact]
        public void GetAllAsSelectListItems_Should_Work_Correctly()
        {
            // Arrange
            List<Subject> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Subject>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ISubjectsService subjectsService = new SubjectsService(mockedRepository.Object);
            IEnumerable<string> expectedNames = testData.OrderBy(x => x.Name).Select(x => x.Name);
            IEnumerable<string> expectedValues = testData.OrderBy(x => x.Name).Select(x => x.Id.ToString());

            // Act
            IEnumerable<SelectListItem> actual = subjectsService.GetAllAsSelectListItems();

            // Assert
            Assert.Equal(expectedNames, actual.Select(x => x.Text));
            Assert.Equal(expectedValues, actual.Select(x => x.Value));
        }

        private List<Subject> GetTestData()
        {
            List<Subject> subjects = new List<Subject>()
            {
                new Subject
                {
                    Id = 1,
                    Name = "Maths",
                },
                new Subject
                {
                    Id = 2,
                    Name = "English",
                },
            };

            return subjects;
        }
    }
}
