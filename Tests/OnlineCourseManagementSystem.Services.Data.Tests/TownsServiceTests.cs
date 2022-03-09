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

    public class TownsServiceTests
    {
        [Fact]
        public void GetAllAsSelectListItems_Should_Work_Correctly()
        {
            // Arrange
            List<Town> testData = this.GetTownsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Town>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITownsService townsService = new TownsService(mockedRepository.Object);
            IEnumerable<string> expectedTexts = testData.OrderBy(x => x.Name).Select(x => x.Name).ToList();
            IEnumerable<string> expectedValues = testData.OrderBy(x => x.Name).Select(x => x.Id.ToString()).ToList();

            // Act
            IEnumerable<SelectListItem> actual = townsService.GetAllAsSelectListItems();

            // Assert
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
            Assert.Equal(expectedValues, actual.Select(x => x.Value));
        }

        private List<Town> GetTownsTestData()
        {
            List<Town> towns = new List<Town>()
            {
                new Town
                {
                    Id = 1,
                    Name = "Smolyan",
                },
                new Town
                {
                    Id = 2,
                    Name = "Orsk",
                },
            };

            return towns;
        }
    }
}
