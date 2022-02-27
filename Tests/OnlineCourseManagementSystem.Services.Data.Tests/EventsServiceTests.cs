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
    using OnlineCourseManagementSystem.Web.ViewModels.Events;
    using Xunit;

    public class EventsServiceTests
    {
        public EventsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1")]
        public void GetAllCreatedByUserId(string userId)
        {
            // Arrange
            List<Event> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Event>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IEventsService eventsService = new EventsService(mockedRepository.Object, null);

            IEnumerable<int> expectedIds = testData
                .Where(e => e.CreatorId == userId)
                .OrderByDescending(e => e.CreatedOn)
                .Select(e => e.Id).ToList();

            IEnumerable<string> expectedThemes = testData
                .Where(e => e.CreatorId == userId)
                .OrderByDescending(e => e.CreatedOn)
                .Select(e => e.Theme).ToList();

            // Act
            IEnumerable<EventViewModel> actual = eventsService.GetAllCreatedByUserId<EventViewModel>(userId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(e => e.Id));
            Assert.Equal(expectedThemes, actual.Select(e => e.Theme));
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<Event> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Event>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IEventsService eventsService = new EventsService(mockedRepository.Object, null);

            IEnumerable<int> expectedIds = testData.OrderByDescending(e => e.CreatedOn).Select(e => e.Id).ToList();
            IEnumerable<string> expectedThemes = testData.OrderByDescending(e => e.CreatedOn).Select(e => e.Theme).ToList();

            // Act
            IEnumerable<EventViewModel> actual = eventsService.GetAll<EventViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(e => e.Id));
            Assert.Equal(expectedThemes, actual.Select(e => e.Theme));
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int eventId)
        {
            // Arrange
            List<Event> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Event>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IEventsService eventsService = new EventsService(mockedRepository.Object, null);

            string expectedTheme = testData.FirstOrDefault(e => e.Id == eventId).Theme;
            string expectedCreatorFirstName = testData.FirstOrDefault(e => e.Id == eventId).Creator.FirstName;
            string expectedCeratorLastName = testData.FirstOrDefault(e => e.Id == eventId).Creator.LastName;

            // Act
            EventViewModel actual = eventsService.GetById<EventViewModel>(eventId);

            // Arrange
            Assert.Equal(expectedTheme, actual.Theme);
            Assert.Equal(expectedCreatorFirstName, actual.CreatorFirstName);
            Assert.Equal(expectedCeratorLastName, actual.CreatorLastName);
        }

        [Fact]
        public void GetAllComing_Should_Work_Correctly()
        {
            // Arrange
            List<Event> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Event>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IEventsService eventsService = new EventsService(mockedRepository.Object, null);

            IEnumerable<int> expectedIds = testData.Where(e => e.StartDate >= DateTime.UtcNow && e.IsApproved == true).Select(e => e.Id).ToList();
            IEnumerable<string> expectedThemes = testData.Where(e => e.StartDate >= DateTime.UtcNow && e.IsApproved == true).Select(e => e.Theme).ToList();

            // Act
            IEnumerable<EventViewModel> actual = eventsService.GetAllComing<EventViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(e => e.Id));
            Assert.Equal(expectedThemes, actual.Select(e => e.Theme));
        }

        [Fact]
        public void GetAllFinished_Should_Work_Correctly()
        {
            // Arrange
            List<Event> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Event>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IEventsService eventsService = new EventsService(mockedRepository.Object, null);

            IEnumerable<int> expectedIds = testData.Where(e => e.EndDate <= DateTime.UtcNow && e.IsApproved == true).Select(e => e.Id).ToList();
            IEnumerable<string> expectedThemes = testData.Where(e => e.EndDate <= DateTime.UtcNow && e.IsApproved == true).Select(e => e.Theme).ToList();

            // Act
            IEnumerable<EventViewModel> actual = eventsService.GetAllFinished<EventViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(e => e.Id));
            Assert.Equal(expectedThemes, actual.Select(e => e.Theme));
        }

        [Fact]
        public void GetAllByAdmin_Should_Work_Correctly()
        {
            // Arrange
            List<Event> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Event>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IEventsService eventsService = new EventsService(mockedRepository.Object, null);

            IEnumerable<int> expectedIds = testData.OrderByDescending(e => e.CreatedOn).Select(e => e.Id).ToList();
            IEnumerable<string> expectedThemes = testData.OrderByDescending(e => e.CreatedOn).Select(e => e.Theme).ToList();

            // Act
            IEnumerable<EventViewModel> actual = eventsService.GetAll<EventViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(e => e.Id));
            Assert.Equal(expectedThemes, actual.Select(e => e.Theme));
        }

        private List<Event> GetTestData()
        {
            return new List<Event>()
            {
                 new Event()
                 {
                     Id = 1,
                     Theme = "Some title react.",
                     Creator = new ApplicationUser()
                     {
                         Id = "1",
                         FirstName = "Joma",
                         LastName = "Teca",
                     },
                     CreatorId = "1",
                     CreatedOn = new DateTime(2022, 1, 20),
                 },
                 new Event()
                 {
                     Id = 2,
                     Theme = "Some title asp.net core.",
                     Creator = new ApplicationUser()
                     {
                         Id = "1",
                         FirstName = "Joma",
                         LastName = "Teca",
                     },
                     CreatorId = "1",
                     CreatedOn = new DateTime(2022, 1, 20),
                 },
                 new Event()
                 {
                     Id = 3,
                     Theme = "Some title ruby.",
                     Creator = new ApplicationUser()
                     {
                         Id = "2",
                         FirstName = "Hosg",
                         LastName = "Nosiv",
                     },
                     CreatorId = "2",
                     CreatedOn = new DateTime(2021, 12, 4),
                 },
            };
        }
    }
}
