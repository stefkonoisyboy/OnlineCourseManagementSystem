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
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;
    using Xunit;

    public class QuestionsServiceTests
    {
        public QuestionsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1)]
        public void GetCountByExamId_Should_Work_Correctly(int examId)
        {
            // Arrange
            List<Question> testData = this.GetQuestionsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Question>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IQuestionsService questionsService = new QuestionsService(mockedRepository.Object, null);
            int expectedCount = testData.Count(x => x.ExamId == examId);

            // Act
            int actual = questionsService.GetCountByExamId(examId, "test");

            // Assert
            Assert.Equal(expectedCount, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int questionId)
        {
            // Arrange
            List<Question> testData = this.GetQuestionsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Question>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IQuestionsService questionsService = new QuestionsService(mockedRepository.Object, null);
            string expectedExamName = testData.FirstOrDefault(x => x.Id == questionId).Exam.Name;
            string expectedText = testData.FirstOrDefault(x => x.Id == questionId).Text;

            // Act
            AllQuestionsByExamViewModel actual = questionsService.GetById<AllQuestionsByExamViewModel>(questionId);

            // Assert
            Assert.Equal(expectedExamName, actual.ExamName);
            Assert.Equal(expectedText, actual.Text);
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByExam_Should_Work_Correctly(int examId)
        {
            // Arrange
            List<Question> testData = this.GetQuestionsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Question>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IQuestionsService questionsService = new QuestionsService(mockedRepository.Object, null);
            IEnumerable<string> expectedExamNames = testData.Where(x => x.ExamId == examId).OrderBy(x => x.Id).Select(x => x.Exam.Name).ToList();
            IEnumerable<string> expectedTexts = testData.Where(x => x.ExamId == examId).OrderBy(x => x.Id).Select(x => x.Text).ToList();

            // Act
            IEnumerable<AllQuestionsByExamViewModel> actual = questionsService.GetAllByExam<AllQuestionsByExamViewModel>(examId, 1, "test");

            // Assert
            Assert.Equal(expectedExamNames, actual.Select(x => x.ExamName));
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
        }

        private List<Question> GetQuestionsTestData()
        {
            List<Question> questions = new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    Exam = new Exam
                    {
                        Id = 1,
                        Name = "Final exam",
                    },
                    ExamId = 1,
                    Text = "Sample",
                },
                new Question
                {
                    Id = 2,
                    Exam = new Exam
                    {
                        Id = 1,
                        Name = "Final exam",
                    },
                    ExamId = 1,
                    Text = "Random",
                },
            };

            return questions;
        }
    }
}
