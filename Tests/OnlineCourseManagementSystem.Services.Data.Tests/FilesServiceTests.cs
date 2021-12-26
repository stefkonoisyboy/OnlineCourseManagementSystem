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
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Data.Tests.Common;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using Xunit;

    public class FilesServiceTests
    {

        public FilesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1, "1")]
        public async Task DeleteImageFromGallery_Shoud_Work_Correctly(int fileId, string userId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            mockedRepository.Setup(x => x.Delete(It.IsAny<File>()))
                            .Callback((File file) => file.IsDeleted = true);
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null);

            // Act
            await filesService.DeleteImageFromGallery(fileId, userId);

            // Arrange
            Assert.True(testData.FirstOrDefault(f => f.Id == fileId && f.UserId == userId).IsDeleted);
        }

        [Theory]
        [InlineData(1, 1)]
        public void GetAllById_Should_Work_Correctly(int lectureId, int id)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null);
            IEnumerable<string> expectedRemoteUrls = testData
                .Where(f => f.LectureId == lectureId && f.Id != id)
                .Select(f => f.RemoteUrl)
                .ToList();
            IEnumerable<string> expectedExtensions = testData
                .Where(f => f.LectureId == lectureId && f.Id != id)
                .Select(f => f.Extension)
                .ToList();
            IEnumerable<int> expectedIds = testData
                .Where(f => f.LectureId == lectureId && f.Id != id)
                .Select(f => f.Id)
                .ToList();

            // Act
            IEnumerable<FileViewModel> actual = filesService.GetAllById<FileViewModel>(lectureId, id);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(f => f.Id));
            Assert.Equal(expectedRemoteUrls, actual.Select(f => f.RemoteUrl));
            Assert.Equal(expectedExtensions, actual.Select(f => f.Extension));
        }

        [Theory]
        [InlineData(1, "1")]
        public void GetAllResourceFilesByAssignment_Should_Work_Correctly(int assignmentId, string userId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(f => f.All()).Returns(testData.AsQueryable());
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null);
            IEnumerable<int> expectedIds = testData
                .Where(f => f.AssignmentId == assignmentId && f.Type == FileType.Resource)
                .Select(f => f.Id)
                .ToList();
            IEnumerable<string> expectedRemoteUrls = testData
                .Where(f => f.AssignmentId == assignmentId && f.Type == FileType.Resource)
                .Select(f => f.RemoteUrl)
                .ToList();
            IEnumerable<string> expectedExtensions = testData
                .Where(f => f.AssignmentId == assignmentId && f.Type == FileType.Resource)
                .Select(f => f.Extension)
                .ToList();

            // Act
            IEnumerable<FileViewModel> actual = filesService.GetAllResourceFilesByAssignemt<FileViewModel>(assignmentId, userId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(f => f.Id));
            Assert.Equal(expectedRemoteUrls, actual.Select(f => f.RemoteUrl));
            Assert.Equal(expectedExtensions, actual.Select(f => f.Extension));
        }

        [Theory]
        [InlineData("1", 1)]
        public void GetAllImagesForUser_Should_Work_Correctly(string userId, int albumId)
        {
            // Arrange
            List<Album> testData = this.GetAlbumsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Album>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IFilesService filesService = new FilesService(null, null, null, mockedRepository.Object);
            string expectedName = testData.FirstOrDefault(a => a.Id == albumId && a.UserId == userId).Name;
            int expectedId = albumId;
            IEnumerable<string> expectedImagesRemoteUrls = testData.FirstOrDefault(a => a.Id == albumId && a.UserId == userId).Images.Select(f => f.RemoteUrl).ToList();
            IEnumerable<int> expectedImagesIds = testData.FirstOrDefault(x => x.Id == albumId).Images.Select(f => f.Id).ToList();

            // Act
            AllImagesViewModel actual = filesService.GetAllImagesForUserByAlbum<AllImagesViewModel>(userId, albumId);

            // Arrange
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedName, actual.Name);
            Assert.Equal(expectedImagesIds, actual.Images.Select(x => x.ImageId));
            Assert.Equal(expectedImagesRemoteUrls, actual.Images.Select(x => x.ImageUrl));
        }

        private List<File> GetAllFilesTestData()
        {
            List<File> files = new List<File>
            {
                new File
                {
                    Id = 1,
                    UserId = "1",
                    LectureId = 1,
                },
                new File
                {
                    Id = 2,
                    UserId = "1",
                    LectureId = 1,
                },
                new File
                {
                    Id = 3,
                    UserId = "2",
                    LectureId = 2,
                },
            };

            return files;
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
