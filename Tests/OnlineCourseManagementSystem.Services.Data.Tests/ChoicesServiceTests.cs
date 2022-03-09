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
    using OnlineCourseManagementSystem.Web.ViewModels.Choices;
    using Xunit;

    public class ChoicesServiceTests
    {
        public ChoicesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1)]
        public void GetAllById_Should_Work_Correctly(int questionId)
        {
            // Arrange
            List<Choice> testData = this.GetChoicesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Choice>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IChoicesService choicesService = new ChoicesService(mockedRepository.Object);
            IEnumerable<int> expectedIds = testData.Where(x => x.QuestionId == questionId).Select(x => x.Id).ToList();
            IEnumerable<string> expectedTexts = testData.Where(x => x.QuestionId == questionId).Select(x => x.Text).ToList();
            IEnumerable<bool> expectedIsCorrects = testData.Where(x => x.QuestionId == questionId).Select(x => x.IsCorrect).ToList();

            // Act
            IEnumerable<AllChoicesByIdViewModel> actual = choicesService.GetAllById<AllChoicesByIdViewModel>(questionId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
            Assert.Equal(expectedIsCorrects, actual.Select(x => x.IsCorrect));
        }

        private List<Choice> GetChoicesTestData()
        {
            List<Choice> choices = new List<Choice>()
            {
                new Choice
                {
                    Id = 1,
                    Text = "A",
                    IsCorrect = true,
                    QuestionId = 1,
                },
                new Choice
                {
                    Id = 2,
                    Text = "B",
                    IsCorrect = false,
                    QuestionId = 1,
                },
            };

            return choices;
        }
    }
}
