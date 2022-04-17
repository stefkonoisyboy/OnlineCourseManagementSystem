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
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);

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
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);
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
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);
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
        public void GetAllImagesForUserByAlbum_Should_Work_Correctly(string userId, int albumId)
        {
            // Arrange
            List<Album> testData = this.GetAlbumsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Album>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IFilesService filesService = new FilesService(null, null, null, mockedRepository.Object, null, null);
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

        [Theory]
        [InlineData(1, "1")]
        public void GetAllUserSubmittedFilesForAssignment_Should_Work_Correctly(int assignmentId, string userId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);
            IEnumerable<int> expectedIds = testData.Where(f => f.AssignmentId == assignmentId && f.UserId == userId && f.Type == FileType.Submit).Select(f => f.Id).ToList();
            IEnumerable<string> expectedRemoteUrls = testData.Where(f => f.AssignmentId == assignmentId && f.UserId == userId && f.Type == FileType.Submit).Select(f => f.RemoteUrl).ToList();
            IEnumerable<string> expectedExtensions = testData.Where(f => f.AssignmentId == assignmentId && f.UserId == userId && f.Type == FileType.Submit).Select(f => f.Extension).ToList();

            // Act
            IEnumerable<FileViewModel> actual = filesService.GetAllUserSubmittedFilesForAssignment<FileViewModel>(assignmentId, userId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(f => f.Id));
            Assert.Equal(expectedRemoteUrls, actual.Select(f => f.RemoteUrl));
            Assert.Equal(expectedExtensions, actual.Select(f => f.Extension));
        }

        [Theory]
        [InlineData(2)]
        public async Task DeleteWorkFileFromAssignment_Should_Work_Correctly(int fileId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            mockedRepository.Setup(x => x.Delete(It.IsAny<File>()))
                .Callback((File file) =>
                {
                    file.IsDeleted = true;
                });
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);
            int? expectedAssignemntId = testData.FirstOrDefault(f => f.Id == fileId).AssignmentId;

            // Act
            int? actualAssigmentId = await filesService.DeleteWorkFileFromAssignment(fileId);

            // Arrange
            Assert.True(testData.FirstOrDefault(f => f.Id == fileId).IsDeleted);
        }

        [Theory]
        [InlineData(3)]
        public async Task DeleteFromEventAsync(int fileId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            mockedRepository.Setup(x => x.Delete(It.IsAny<File>()))
                .Callback((File file) =>
                {
                    file.IsDeleted = true;
                });
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);
            int? expectedEventId = testData.FirstOrDefault(f => f.Id == fileId).EventId;

            // Act
            int? actualEventId = await filesService.DeleteFromEventAsync(fileId);

            // Arrange
            Assert.Equal(expectedEventId, actualEventId);
            Assert.True(testData.FirstOrDefault(f => f.Id == fileId).IsDeleted);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_Should_Work_Correctly(int fileId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(f => f.All()).Returns(testData.AsQueryable());
            mockedRepository.Setup(f => f.Delete(It.IsAny<File>()))
                .Callback((File file) =>
                {
                    file.IsDeleted = true;
                });
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);
            int? expectedLectureId = testData.FirstOrDefault(f => f.Id == fileId).LectureId;

            // Act
            int? actualLectureId = await filesService.DeleteAsync(fileId);

            // Arrange
            Assert.Equal(expectedLectureId, actualLectureId);
            Assert.True(testData.FirstOrDefault(f => f.Id == fileId).IsDeleted);
        }

        [Theory]
        [InlineData(1)]
        public void GetRemoteUrlById_Should_Work_Correctly(int fileId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedReository = new Mock<IDeletableEntityRepository<File>>();
            mockedReository.Setup(f => f.All()).Returns(testData.AsQueryable());
            IFilesService filesService = new FilesService(null, mockedReository.Object, null, null, null, null);
            string expectedRemoteUrl = testData.FirstOrDefault(f => f.Id == fileId).RemoteUrl;

            // Act
            string actualRemoteUrl = filesService.GetRemoteUrlById(fileId);

            // Arrange
            Assert.Equal(expectedRemoteUrl, actualRemoteUrl);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int filedId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);
            int expectedId = testData.FirstOrDefault(f => f.Id == filedId).Id;
            string expectedRemoteUrl = testData.FirstOrDefault(f => f.Id == filedId).RemoteUrl;
            string expectedExtension = testData.FirstOrDefault(f => f.Id == filedId).Extension;

            // Act
            FileViewModel actual = filesService.GetById<FileViewModel>(filedId);

            // Arrange
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedRemoteUrl, actual.RemoteUrl);
            Assert.Equal(expectedExtension, actual.Extension);
        }

        [Theory]
        [InlineData("url5", 1)]
        public async Task AddVideoResourceToEventAsync_Should_Work_Correctly(string remoteUrl, int eventId)
        {
            // Arrange
            List<File> testData = this.GetAllFilesTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<File>()))
                .Callback((File file) =>
                {
                    testData.Add(file);
                    testData.Last().Id = testData.Count;
                });
            VideoFileInputModel inputModel = new VideoFileInputModel
            {
                RemoteUrl = remoteUrl,
                EventId = eventId,
            };
            IFilesService filesService = new FilesService(null, mockedRepository.Object, null, null, null, null);

            // Act
            int actualFileId = await filesService.AddVideoResourceToEventAsync(inputModel);

            // Arrange
            Assert.Equal(testData.Last().Id, actualFileId);
            Assert.Contains(testData, f => f.Id == testData.ToList().Count);
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
                    IsDeleted = false,
                    EventId = null,
                },
                new File
                {
                    Id = 2,
                    UserId = "1",
                    LectureId = 1,
                    IsDeleted = false,
                    AssignmentId = 2,
                    EventId = null,
                },
                new File
                {
                    Id = 3,
                    UserId = "2",
                    IsDeleted = false,
                    EventId = 1,
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
