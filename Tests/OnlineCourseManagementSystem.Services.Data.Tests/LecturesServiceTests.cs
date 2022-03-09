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
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using Xunit;

    public class LecturesServiceTests
    {
        public LecturesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData("1")]
        public void GetLecturesCountByCreatorId_Should_Work_Correctly(string creatorId)
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            int expectedCount = lecturesTestData.Where(x => x.CreatorId == creatorId).Count();

            // Act
            int actual = lecturesService.GetLecturesCountByCreatorId(creatorId);

            // Assert
            Assert.Equal(expectedCount, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetCourseIdByLectureId_Should_Work_Correcly(int lectureId)
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            int expectedCourseId = lecturesTestData.FirstOrDefault(x => x.Id == lectureId).CourseId;

            // Act
            int actual = lecturesService.GetCourseIdByLectureId(lectureId);

            // Assert
            Assert.Equal(expectedCourseId, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetNameById_Should_Work_Correcly(int lectureId)
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            string expectedName = lecturesTestData.FirstOrDefault(x => x.Id == lectureId).Title;

            // Act
            string actual = lecturesService.GetNameById(lectureId);

            // Assert
            Assert.Equal(expectedName, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correcly(int lectureId)
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            int expectedId = lecturesTestData.FirstOrDefault(x => x.Id == lectureId).Id;
            string expectedTitle = lecturesTestData.FirstOrDefault(x => x.Id == lectureId).Title;
            DateTime expectedModifiedOn = lecturesTestData.FirstOrDefault(x => x.Id == lectureId).ModifiedOn.Value;
            string expectedCourseFileRemoteUrl = lecturesTestData.FirstOrDefault(x => x.Id == lectureId).Course.File.RemoteUrl;

            // Act
            AllLecturesByCreatorIdViewModel actual = lecturesService.GetById<AllLecturesByCreatorIdViewModel>(lectureId);

            // Assert
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedTitle, actual.Title);
            Assert.Equal(expectedModifiedOn, actual.ModifiedOn);
            Assert.Equal(expectedCourseFileRemoteUrl, actual.CourseFileRemoteUrl);
        }

        [Theory]
        [InlineData(1)]
        public void GetAllById_Should_Work_Correcly(int courseId)
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            IEnumerable<int> expectedIds = lecturesTestData.Where(x => x.CourseId == courseId).Select(x => x.Id).ToList();
            IEnumerable<string> expectedTitles = lecturesTestData.Where(x => x.CourseId == courseId).Select(x => x.Title).ToList();
            IEnumerable<DateTime> expectedModifiedOns = lecturesTestData.Where(x => x.CourseId == courseId).Select(x => x.ModifiedOn.Value).ToList();
            IEnumerable<string> expectedCourseFileRemoteUrls = lecturesTestData.Where(x => x.CourseId == courseId).Select(x => x.Course.File.RemoteUrl).ToList();

            // Act
            IEnumerable<AllLecturesByCreatorIdViewModel> actual = lecturesService.GetAllById<AllLecturesByCreatorIdViewModel>(courseId, 1, 100000);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedTitles, actual.Select(x => x.Title));
            Assert.Equal(expectedModifiedOns, actual.Select(x => x.ModifiedOn));
            Assert.Equal(expectedCourseFileRemoteUrls, actual.Select(x => x.CourseFileRemoteUrl));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllByFileId_Should_Work_Correcly(int fileId)
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            IEnumerable<int> expectedIds = lecturesTestData.OrderBy(x => x.CreatedOn).Where(x => x.Files.Any(f => f.Id == fileId)).Select(x => x.Id).ToList();
            IEnumerable<string> expectedTitles = lecturesTestData.OrderBy(x => x.CreatedOn).Where(x => x.Files.Any(f => f.Id == fileId)).Select(x => x.Title).ToList();
            IEnumerable<DateTime> expectedModifiedOns = lecturesTestData.OrderBy(x => x.CreatedOn).Where(x => x.Files.Any(f => f.Id == fileId)).Select(x => x.ModifiedOn.Value).ToList();
            IEnumerable<string> expectedCourseFileRemoteUrls = lecturesTestData.OrderBy(x => x.CreatedOn).Where(x => x.Files.Any(f => f.Id == fileId)).Select(x => x.Course.File.RemoteUrl).ToList();

            // Act
            IEnumerable<AllLecturesByCreatorIdViewModel> actual = lecturesService.GetAllByFileId<AllLecturesByCreatorIdViewModel>(fileId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedTitles, actual.Select(x => x.Title));
            Assert.Equal(expectedModifiedOns, actual.Select(x => x.ModifiedOn));
            Assert.Equal(expectedCourseFileRemoteUrls, actual.Select(x => x.CourseFileRemoteUrl));
        }

        [Theory]
        [InlineData("1")]
        public void GetAllByCreatorId_Should_Work_Correcly(string creatorId)
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            IEnumerable<int> expectedIds = lecturesTestData.OrderByDescending(x => x.CreatedOn).Skip((1 - 1) * 3).Take(3).Where(x => x.CreatorId == creatorId && x.Course.CreatorId == creatorId).Select(x => x.Id).ToList();
            IEnumerable<string> expectedTitles = lecturesTestData.OrderByDescending(x => x.CreatedOn).Skip((1 - 1) * 3).Take(3).Where(x => x.CreatorId == creatorId && x.Course.CreatorId == creatorId).Select(x => x.Title).ToList();
            IEnumerable<DateTime> expectedModifiedOns = lecturesTestData.OrderByDescending(x => x.CreatedOn).Skip((1 - 1) * 3).Take(3).Where(x => x.CreatorId == creatorId && x.Course.CreatorId == creatorId).Select(x => x.ModifiedOn.Value).ToList();
            IEnumerable<string> expectedCourseFileRemoteUrls = lecturesTestData.OrderByDescending(x => x.CreatedOn).Skip((1 - 1) * 3).Take(3).Where(x => x.CreatorId == creatorId && x.Course.CreatorId == creatorId).Select(x => x.Course.File.RemoteUrl).ToList();

            // Act
            IEnumerable<AllLecturesByCreatorIdViewModel> actual = lecturesService.GetAllByCreatorId<AllLecturesByCreatorIdViewModel>(1, creatorId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedTitles, actual.Select(x => x.Title));
            Assert.Equal(expectedModifiedOns, actual.Select(x => x.ModifiedOn));
            Assert.Equal(expectedCourseFileRemoteUrls, actual.Select(x => x.CourseFileRemoteUrl));
        }

        [Theory]
        [InlineData(1)]
        public void GetAllVideosById_Should_Work_Correctly(int lectureId)
        {
            // Arrange
            List<File> videosTestData = this.GetVideosTestData();
            var mockedFilesRepository = new Mock<IDeletableEntityRepository<File>>();
            mockedFilesRepository.Setup(x => x.All()).Returns(videosTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(null, mockedFilesRepository.Object, null);
            IEnumerable<string> expectedRemoteUrls = videosTestData.Where(x => x.LectureId == lectureId && x.Extension == ".mp4").Select(x => x.RemoteUrl).ToList();

            // Act
            IEnumerable<AllVideosByIdViewModel> actual = lecturesService.GetAllVideosById<AllVideosByIdViewModel>(lectureId);

            // Assert
            Assert.Equal(expectedRemoteUrls, actual.Select(x => x.RemoteUrl));
        }

        [Fact]
        public void GetAllInInterval_Should_Work_Correcly()
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            IEnumerable<int> expectedIds = lecturesTestData.Where(x => x.StartDate >= new DateTime(2022, 1, 10) && x.EndDate <= new DateTime(2022, 1, 15)).OrderBy(x => x.StartDate).ThenBy(x => x.EndDate).Select(x => x.Id).ToList();
            IEnumerable<string> expectedTitles = lecturesTestData.Where(x => x.StartDate >= new DateTime(2022, 1, 10) && x.EndDate <= new DateTime(2022, 1, 15)).OrderBy(x => x.StartDate).ThenBy(x => x.EndDate).Select(x => x.Title).ToList();
            IEnumerable<DateTime> expectedModifiedOns = lecturesTestData.Where(x => x.StartDate >= new DateTime(2022, 1, 10) && x.EndDate <= new DateTime(2022, 1, 15)).OrderBy(x => x.StartDate).ThenBy(x => x.EndDate).Select(x => x.ModifiedOn.Value).ToList();
            IEnumerable<string> expectedCourseFileRemoteUrls = lecturesTestData.Where(x => x.StartDate >= new DateTime(2022, 1, 10) && x.EndDate <= new DateTime(2022, 1, 15)).OrderBy(x => x.StartDate).ThenBy(x => x.EndDate).Select(x => x.Course.File.RemoteUrl).ToList();

            // Act
            IEnumerable<AllLecturesByCreatorIdViewModel> actual = lecturesService.GetAllInInterval<AllLecturesByCreatorIdViewModel>(new DateTime(2022, 1, 10), new DateTime(2022, 1, 15));

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedTitles, actual.Select(x => x.Title));
            Assert.Equal(expectedModifiedOns, actual.Select(x => x.ModifiedOn));
            Assert.Equal(expectedCourseFileRemoteUrls, actual.Select(x => x.CourseFileRemoteUrl));
        }

        [Fact]
        public void GetAllByAdmin_Should_Work_Correctly()
        {
            // Arrange
            List<Lecture> lecturesTestData = this.GetLecturesTestData();
            var mockedLecturesRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            mockedLecturesRepository.Setup(x => x.All()).Returns(lecturesTestData.AsQueryable());
            ILecturesService lecturesService = new LecturesService(mockedLecturesRepository.Object, null, null);
            IEnumerable<int> expectedIds = lecturesTestData.OrderByDescending(x => x.CreatedOn).Select(x => x.Id).ToList();
            IEnumerable<string> expectedTitles = lecturesTestData.OrderByDescending(x => x.CreatedOn).Select(x => x.Title).ToList();
            IEnumerable<DateTime> expectedModifiedOns = lecturesTestData.OrderByDescending(x => x.CreatedOn).Select(x => x.ModifiedOn.Value).ToList();
            IEnumerable<string> expectedCourseFileRemoteUrls = lecturesTestData.OrderByDescending(x => x.CreatedOn).Select(x => x.Course.File.RemoteUrl).ToList();

            // Act
            IEnumerable<AllLecturesByCreatorIdViewModel> actual = lecturesService.GetAllByAdmin<AllLecturesByCreatorIdViewModel>();

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedTitles, actual.Select(x => x.Title));
            Assert.Equal(expectedModifiedOns, actual.Select(x => x.ModifiedOn));
            Assert.Equal(expectedCourseFileRemoteUrls, actual.Select(x => x.CourseFileRemoteUrl));
        }

        private List<Lecture> GetLecturesTestData()
        {
            List<Lecture> lectures = new List<Lecture>()
            {
                new Lecture
                {
                    Id = 1,
                    Title = "Auto Mapper",
                    ModifiedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                    StartDate = new DateTime(2022, 1, 10),
                    EndDate = new DateTime(2022, 1, 14),
                    Course = new Course
                    {
                        Id = 1,
                        File = new File
                        {
                            Id = 1,
                            RemoteUrl = "remote",
                        },
                        FileId = 1,
                    },
                    CreatorId = "1",
                },
                new Lecture
                {
                    Id = 2,
                    Title = "Auto Mapper",
                    ModifiedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                    StartDate = new DateTime(2023, 1, 10),
                    EndDate = new DateTime(2023, 1, 14),
                    Course = new Course
                    {
                        Id = 2,
                        File = new File
                        {
                            Id = 2,
                            RemoteUrl = "remote",
                        },
                        FileId = 2,
                    },
                    CreatorId = "2",
                },
            };

            return lectures;
        }

        private List<File> GetVideosTestData()
        {
            List<File> videos = new List<File>()
            {
                new File
                {
                    Id = 1,
                    LectureId = 1,
                    Extension = ".mp4",
                    RemoteUrl = "remote",
                },
                new File
                {
                    Id = 2,
                    LectureId = 2,
                    Extension = ".mp4",
                    RemoteUrl = "remote",
                },
            };

            return videos;
        }
    }
}
