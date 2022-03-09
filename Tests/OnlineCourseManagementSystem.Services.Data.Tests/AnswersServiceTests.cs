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
    using OnlineCourseManagementSystem.Web.ViewModels.Answers;
    using Xunit;

    public class AnswersServiceTests
    {
        public AnswersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1, "1")]
        public void GetAllByExamIdAndUserId_Should_Work_Correctly(int examId, string userId)
        {
            // Arrange
            List<Answer> testData = this.GetAnswersTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Answer>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAnswersService answersService = new AnswersService(mockedRepository.Object);
            IEnumerable<string> expectedTexts = testData.Where(a => a.Question.ExamId == examId && a.UserId == userId).Select(x => x.Text).ToList();
            IEnumerable<int> expectedQuestionIds = testData.Where(a => a.Question.ExamId == examId && a.UserId == userId).Select(x => x.QuestionId).ToList();

            // Act
            IEnumerable<AllAnswersByExamIdAndUserIdViewModel> actual = answersService.GetAllByExamIdAndUserId<AllAnswersByExamIdAndUserIdViewModel>(examId, userId);

            // Assert
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
            Assert.Equal(expectedQuestionIds, actual.Select(x => x.QuestionId));
        }

        private List<Answer> GetAnswersTestData()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer
                {
                    Id = 1,
                    Text = "A",
                    Question = new Question
                    {
                        Id = 1,
                        ExamId = 1,
                    },
                    QuestionId = 1,
                    UserId = "1",
                },
                new Answer
                {
                    Id = 2,
                    Text = "B",
                    Question = new Question
                    {
                        Id = 1,
                        ExamId = 1,
                    },
                    QuestionId = 1,
                    UserId = "2",
                },
            };

            return answers;
        }
    }
}
