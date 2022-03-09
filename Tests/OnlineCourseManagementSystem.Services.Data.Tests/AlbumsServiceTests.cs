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
    using OnlineCourseManagementSystem.Web.ViewModels.Albums;
    using Xunit;

    public class AlbumsServiceTests
    {
        public AlbumsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1")]
        public void GetAllById_Should_Work_Correctly(string userId)
        {
            // Arrange
            List<Album> testData = this.GetAlbumsTestData();
            var mockedRepsitory = new Mock<IDeletableEntityRepository<Album>>();
            mockedRepsitory.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAlbumsService albumsService = new AlbumsService(mockedRepsitory.Object);
            IEnumerable<string> expectedNames = testData.Where(a => a.UserId == userId).Select(a => a.Name).ToList();
            IEnumerable<int> expectedIds = testData.Where(a => a.UserId == userId).Select(a => a.Id).ToList();

            // Act
            IEnumerable<AlbumViewModel> actual = albumsService.GetAllById<AlbumViewModel>(userId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.AlbumId));
            Assert.Equal(expectedNames, actual.Select(a => a.Name));
        }

        [Theory]
        [InlineData("1", 1, "testMyMethod")]
        public async Task UpdateAsync_Should_Work_Correctly(string userId, int albumId, string name)
        {
            // Arrange
            List<Album> testData = this.GetAlbumsTestData();
            var mockedRepsitory = new Mock<IDeletableEntityRepository<Album>>();
            mockedRepsitory.Setup(x => x.All()).Returns(testData.AsQueryable());
            mockedRepsitory.Setup(x => x.Update(It.IsAny<Album>()))
                .Callback((Album album) =>
                {
                    album.Name = name;
                });
            IAlbumsService albumsService = new AlbumsService(mockedRepsitory.Object);
            EditAlbumInputModel inputModel = new EditAlbumInputModel
            {
                Id = albumId,
                Name = name,
                UserId = userId,
            };

            // Act
            await albumsService.UpdateAsync(inputModel);

            // Arrange
            Assert.True(testData.FirstOrDefault(a => a.Id == albumId).Name == name);
        }

        [Theory]
        [InlineData("1", "testMyMethod")]
        public async Task CreateAsync_Should_Work_Correctly(string userId, string name)
        {
            // Arrange
            List<Album> testData = this.GetAlbumsTestData();
            var mockedRepsitory = new Mock<IDeletableEntityRepository<Album>>();
            mockedRepsitory.Setup(x => x.All()).Returns(testData.AsQueryable());
            mockedRepsitory.Setup(x => x.AddAsync(It.IsAny<Album>()))
                .Callback((Album album) =>
                {
                    testData.Add(album);
                });
            IAlbumsService albumsService = new AlbumsService(mockedRepsitory.Object);
            AlbumInputModel inputModel = new AlbumInputModel
            {
                UserId = userId,
                Name = name,
            };

            // Act
            await albumsService.CreateAsync(inputModel);

            // Arrange
            Assert.Contains(testData, a => a.UserId == userId && a.Name == name);
        }

        private List<Album> GetAlbumsTestData()
        {
            List<Album> albums = new List<Album>
            {
                new Album
                {
                    Id = 1,
                    Name = "test1",
                    UserId = "1",
                    Images = new HashSet<File>
                    {
                        new File
                        {
                            Id = 1,
                            RemoteUrl = "url1",
                            AlbumId = 1,
                        },
                        new File
                        {
                            Id = 2,
                            RemoteUrl = "url2",
                            AlbumId = 1,
                        },
                    },
                },
                new Album
                {
                    Id = 2,
                    Name = "test2",
                    UserId = "1",
                    Images = new HashSet<File>
                    {
                        new File
                        {
                            Id = 3,
                            RemoteUrl = "url3",
                            AlbumId = 2,
                        },
                        new File
                        {
                            Id = 4,
                            RemoteUrl = "url4",
                            AlbumId = 2,
                        },
                    },
                },
            };

            return albums;
        }
    }
}
