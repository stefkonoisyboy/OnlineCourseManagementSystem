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
    using OnlineCourseManagementSystem.Web.ViewModels.Skills;
    using Xunit;

    public class SkillsServiceTests
    {
        public SkillsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByCourseId_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<Skill> testData = this.GetSkillsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Skill>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ISkillsService skillsService = new SkillsService(mockedRepository.Object);
            IEnumerable<string> expectedTexts = testData.Where(x => x.CourseId == courseId).OrderByDescending(x => x.CreatedOn).Select(x => x.Text).ToList();

            // Act
            IEnumerable<AllSkillsByCourseIdViewModel> actual = skillsService.GetAllByCourseId<AllSkillsByCourseIdViewModel>(courseId);

            // Assert
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
        }

        private List<Skill> GetSkillsTestData()
        {
            List<Skill> skills = new List<Skill>()
            {
                new Skill
                {
                    Id = 1,
                    Text = "Github",
                    CourseId = 1,
                },
                new Skill
                {
                    Id = 2,
                    Text = "Azure",
                    CourseId = 1,
                },
            };

            return skills;
        }
    }
}
