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
    using OnlineCourseManagementSystem.Services.Data.Tests.Common;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;
    using Xunit;

    public class TagsServiceTests
    {
        public TagsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void GetAllAsSelectListItems_Should_Work_Correctly()
        {
            // Arrange
            List<Tag> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Tag>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITagsService tagsService = new TagsService(mockedRepository.Object, null);
            IEnumerable<string> expectedNames = testData.OrderBy(x => x.Name).Select(x => x.Name);
            IEnumerable<string> expectedValues = testData.OrderBy(x => x.Name).Select(x => x.Id.ToString());

            // Act
            IEnumerable<SelectListItem> actual = tagsService.GetAllAsSelectListItems();

            // Assert
            Assert.Equal(expectedNames, actual.Select(x => x.Text));
            Assert.Equal(expectedValues, actual.Select(x => x.Value));
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<Tag> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Tag>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITagsService tagsService = new TagsService(mockedRepository.Object, null);
            IEnumerable<string> expectedNames = testData.OrderBy(x => x.Name).Select(x => x.Name);

            // Act
            IEnumerable<AllTagsViewModel> actual = tagsService.GetAll<AllTagsViewModel>();

            // Assert
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByCourseId_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<CourseTag> testData = this.GetCourseTagTestData();

            var mockedRepository = new Mock<IDeletableEntityRepository<CourseTag>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITagsService tagsService = new TagsService(null, mockedRepository.Object);
            IEnumerable<string> expectedNames = testData.Where(x => x.CourseId == courseId).OrderBy(x => x.Tag.Name).Select(x => x.Tag.Name);

            // Act
            IEnumerable<AllTagsByCourseIdViewModel> actual = tagsService.GetAllByCourseId<AllTagsByCourseIdViewModel>(courseId);

            // Assert
            Assert.Equal(expectedNames, actual.Select(x => x.TagName));
        }

        private List<Tag> GetTestData()
        {
            List<Tag> testData = new List<Tag>()
            {
                new Tag
                {
                    Id = 1,
                    Name = "Web",
                },
                new Tag
                {
                    Id = 2,
                    Name = "AI",
                },
            };

            return testData;
        }

        private List<CourseTag> GetCourseTagTestData()
        {
            List<CourseTag> courseTags = new List<CourseTag>()
            {
                new CourseTag
                {
                   Course = new Course
                   {
                       Id = 1,
                   },
                   CourseId = 1,
                   Tag = new Tag
                   {
                       Id = 1,
                       Name = "Web",
                   },
                   TagId = 1,
                },
                new CourseTag
                {
                   Course = new Course
                   {
                       Id = 2,
                   },
                   CourseId = 2,
                   Tag = new Tag
                   {
                       Id = 2,
                       Name = "AI",
                   },
                   TagId = 2,
                },
            };

            return courseTags;
        }
    }
}
