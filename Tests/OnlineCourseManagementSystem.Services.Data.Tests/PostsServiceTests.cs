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
    using OnlineCourseManagementSystem.Web.ViewModels.Likes;
    using OnlineCourseManagementSystem.Web.ViewModels.Posts;
    using Xunit;

    public class PostsServiceTests
    {
        public PostsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void GetAll_Should_Work_Correctly()
        {
            // Arrange
            List<Post> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Post>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IPostsService postsService = new PostsService(mockedRepository.Object, null, null);

            IEnumerable<int> expectedIds = testData.OrderByDescending(p => p.Comments.OrderByDescending(c => c.CreatedOn).FirstOrDefault().CreatedOn)
                .ThenByDescending(p => p.Comments.OrderByDescending(c => c.ModifiedOn).FirstOrDefault().ModifiedOn)
                .ThenByDescending(p => p.CreatedOn).Select(p => p.Id).ToList();
            IEnumerable<string> expectedTitles = testData.OrderByDescending(p => p.Comments.OrderByDescending(c => c.CreatedOn).FirstOrDefault().CreatedOn)
                .ThenByDescending(p => p.Comments.OrderByDescending(c => c.ModifiedOn).FirstOrDefault().ModifiedOn)
                .ThenByDescending(p => p.CreatedOn).Select(p => p.Title).ToList();
            IEnumerable<string> expectedAuthorIds = testData.OrderByDescending(p => p.Comments.OrderByDescending(c => c.CreatedOn).FirstOrDefault().CreatedOn)
                .ThenByDescending(p => p.Comments.OrderByDescending(c => c.ModifiedOn).FirstOrDefault().ModifiedOn)
                .ThenByDescending(p => p.CreatedOn).Select(p => p.AuthorId).ToList();

            // Act
            IEnumerable<PostViewModel> actual = postsService.GetAll<PostViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.Id));
            Assert.Equal(expectedTitles, actual.Select(a => a.Title));
            Assert.Equal(expectedAuthorIds, actual.Select(a => a.AuthorId));
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int postId)
        {
            // Arrange
            List<Post> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Post>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IPostsService postsService = new PostsService(mockedRepository.Object, null, null);

            string expectedTitle = testData.FirstOrDefault(p => p.Id == postId).Title;
            string expectedAuthorId = testData.FirstOrDefault(p => p.Id == postId).AuthorId;

            // Act
            PostViewModel actual = postsService.GetById<PostViewModel>(postId);

            // Arrange
            Assert.Equal(expectedTitle, actual.Title);
            Assert.Equal(expectedAuthorId, actual.AuthorId);
        }

        [Theory]
        [InlineData(2)]
        public void GetAllByCourseId_Should_Work_Correctly(int courseId)
        {
            // Arrange
            List<Post> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Post>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IPostsService postsService = new PostsService(mockedRepository.Object, null, null);

            IEnumerable<int> expectedIds = testData.Where(p => p.CourseId == courseId).Select(p => p.Id).ToList();
            IEnumerable<string> expectedTitles = testData.Where(p => p.CourseId == courseId).Select(p => p.Title).ToList();
            IEnumerable<string> expectedAuthorIds = testData.Where(p => p.CourseId == courseId).Select(p => p.AuthorId).ToList();

            // Act
            IEnumerable<PostViewModel> actual = postsService.GetAllByCourseId<PostViewModel>(courseId);

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.Id));
            Assert.Equal(expectedTitles, actual.Select(a => a.Title));
            Assert.Equal(expectedAuthorIds, actual.Select(a => a.AuthorId));
        }

        [Fact]
        public void GetCountOfAllPosts_Should_Work_Correctly()
        {
            // Arrange
            List<Post> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Post>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IPostsService postsService = new PostsService(mockedRepository.Object, null, null);

            int expectedCount = testData.Count;

            // Act
            int actual = postsService.GetCoutOfAllPosts();

            // Arrange
            Assert.Equal(expectedCount, actual);
        }

        [Fact]
        public void GetAllByAdmin_Should_Work_Correctly()
        {
            // Arrange
            List<Post> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Post>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IPostsService postsService = new PostsService(mockedRepository.Object, null, null);

            IEnumerable<int> expectedIds = testData.OrderByDescending(p => p.CreatedOn).Select(p => p.Id).ToList();
            IEnumerable<string> expectedTitles = testData.OrderByDescending(p => p.CreatedOn).Select(p => p.Title).ToList();
            IEnumerable<string> expectedAuthorIds = testData.OrderByDescending(p => p.CreatedOn).Select(p => p.AuthorId).ToList();

            // Act
            IEnumerable<PostViewModel> actual = postsService.GetAllByAdmin<PostViewModel>();

            // Arrange
            Assert.Equal(expectedIds, actual.Select(a => a.Id));
            Assert.Equal(expectedTitles, actual.Select(a => a.Title));
            Assert.Equal(expectedAuthorIds, actual.Select(a => a.AuthorId));
        }

        [Theory]
        [InlineData("Some content", "New features(ML, AI) OMCS", 1, "1")]
        public async Task CreateAsync_Should_Work_Correctly(string content, string title, int courseId, string authorId)
        {
            // Arrange
            List<Post> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Post>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<Post>()))
                .Callback((Post post) =>
                {
                    testData.Add(post);
                });
            IPostsService postsService = new PostsService(mockedRepository.Object, null, null);
            int expectedPostsCount = testData.ToList().Count + 1;

            CreatePostInputModel inputModel = new CreatePostInputModel()
            {
                Content = content,
                Title = title,
                CourseId = courseId,
                AuthorId = authorId,
            };

            // Act
            await postsService.CreateAsync(inputModel);

            // Arrange
            Assert.Equal(expectedPostsCount, testData.Count);
            Assert.Contains(testData, p => p.Title == title);
            Assert.Contains(testData, p => p.Content == content);
        }

        [Theory]
        [InlineData(1, "New Content", "New Title", 1)]
        public async Task UpdateAsync_Should_Work_Correctly(int postId, string content, string title, int courseId)
        {
            // Arrange
            List<Post> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Post>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            mockedRepository.Setup(x => x.Update(It.IsAny<Post>()))
                .Callback((Post post) =>
                {
                    var oldPost = testData.FirstOrDefault(p => p.Id == postId);
                    oldPost.Content = content;
                    oldPost.Title = title;
                    oldPost.CourseId = courseId;
                });
            IPostsService postsService = new PostsService(mockedRepository.Object, null, null);

            EditPostInputModel inputModel = new EditPostInputModel()
            {
                Id = postId,
                Content = content,
                Title = title,
                CourseId = courseId,
            };

            // Act
            await postsService.UpdateAsync(inputModel);

            // Arrange
            Assert.Contains(testData, p => p.Content == content);
            Assert.Contains(testData, p => p.Title == title);
        }

        private List<Post> GetTestData()
        {
            return new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Author = new ApplicationUser()
                    {
                        Id = "1",
                        FirstName = "Josh",
                        LastName = "Peshov",
                    },
                    CourseId = 1,
                    Title = "Post1",
                    AuthorId = "1",
                    CreatedOn = new DateTime(2021, 3, 2),
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2021, 3, 2),
                            ModifiedOn = new DateTime(2021, 3, 2),
                        },
                        new Comment()
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2021, 4, 2),
                            ModifiedOn = new DateTime(2021, 4, 2),
                        },
                    },
                    Likes = new List<Like>() { },
                    Dislikes = new List<Dislike>() { },
                },
                new Post()
                {
                    Id = 2,
                    Title = "Post2",
                    Author = new ApplicationUser()
                    {
                        Id = "1",
                        FirstName = "Josh",
                        LastName = "Peshov",
                    },
                    CourseId = 2,
                    AuthorId = "1",
                    CreatedOn = new DateTime(2021, 5, 2),
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 5,
                            CreatedOn = new DateTime(2021, 6, 4),
                            ModifiedOn = new DateTime(2021, 7, 4),
                        },
                        new Comment()
                        {
                            Id = 6,
                            CreatedOn = new DateTime(2021, 5, 2),
                        },
                    },
                    Likes = new List<Like>() { },
                    Dislikes = new List<Dislike>() { },
                },
                new Post()
                {
                    Id = 3,
                    Title = "Post3",
                    Author = new ApplicationUser()
                    {
                        Id = "2",
                        FirstName = "Gosho",
                        LastName = "Peshov",
                    },
                    CourseId = 2,
                    AuthorId = "2",
                    CreatedOn = new DateTime(2021, 5, 2),
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 7,
                            CreatedOn = new DateTime(2021, 6, 3),
                        },
                        new Comment()
                        {
                            Id = 8,
                            CreatedOn = new DateTime(2021, 5, 2),
                        },
                    },
                    Likes = new List<Like>() { },
                    Dislikes = new List<Dislike>() { },
                },
            };
        }
    }
}
