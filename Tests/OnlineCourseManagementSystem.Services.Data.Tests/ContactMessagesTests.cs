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
    using OnlineCourseManagementSystem.Web.ViewModels.ContactMessages;
    using Xunit;

    public class ContactMessagesTests
    {
        public ContactMessagesTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<ContactMessage> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ContactMessage>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IContactMessagesService contactMessagesService = new ContactMessagesService(mockedRepository.Object);

            IEnumerable<int> expectedIds = testData.Select(cm => cm.Id).ToList();
            IEnumerable<string> expectedFirstNames = testData.Select(cm => cm.FirstName).ToList();
            IEnumerable<string> expectedLastNames = testData.Select(cm => cm.LastName).ToList();

            // Act
            IEnumerable<ContactMessageViewModel> actual = contactMessagesService.GetAll<ContactMessageViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.Id));
            Assert.Equal(expectedFirstNames, actual.Select(a => a.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(a => a.LastName));
        }

        private List<ContactMessage> GetTestData()
        {
            return new List<ContactMessage>()
            {
                new ContactMessage()
                {
                    Id = 1,
                    Content = "Some content 1",
                    FirstName = "Pesho",
                    LastName = "PeshoSlav",
                    SeenByUser = new ApplicationUser()
                    {
                        Id = "1",
                        FirstName = "Pesho",
                        LastName = "Peshoslavov",
                    },
                },
                new ContactMessage()
                {
                    Id = 1,
                    Content = "Some content 2",
                    FirstName = "Gosho",
                    LastName = "Petkov",
                    SeenByUser = new ApplicationUser()
                    {
                        Id = "1",
                        FirstName = "Pesho",
                        LastName = "Peshoslavov",
                    },
                },
            };
        }
    }
}
