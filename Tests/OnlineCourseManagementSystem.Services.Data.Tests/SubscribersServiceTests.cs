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
    using OnlineCourseManagementSystem.Web.ViewModels.Subscribers;
    using Xunit;

    public class SubscribersServiceTests
    {
        public SubscribersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1")]
        public void GetAll_Should_Work_Correctly(string subscriberId)
        {
            // Arrange
            List<Subscriber> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Subscriber>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ISubscribersService subscribersService = new SubscribersService(mockedRepository.Object);

            string expectedEmail = testData.FirstOrDefault(sb => sb.Id == subscriberId).Email;

            // Act
            SubscriberViewModel actual = subscribersService.GetById<SubscriberViewModel>(subscriberId);

            // Arrange
            Assert.Equal(expectedEmail, actual.Email);
        }

        private List<Subscriber> GetTestData()
        {
            return new List<Subscriber>()
            {
                new Subscriber()
                {
                    Id = "1",
                    Email = "rando_m1@gmail.com",
                },
                new Subscriber()
                {
                    Id = "2",
                    Email = "rando_m2@gmail.com",
                },
            };
        }
    }
}
