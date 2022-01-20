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
    using OnlineCourseManagementSystem.Web.ViewModels.Comments;
    using Xunit;

    public class CommentsServiceTests
    {
        public CommentsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(1)]
        public void GetPostId_Should_Work_Correctly(int commentId)
        {
            // Arrange
            List<Comment> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Comment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICommentsService commentsService = new CommentsService(mockedRepository.Object, null, null);

            int? expectedPostId = testData.FirstOrDefault(c => c.Id == commentId).PostId;

            // Act
            int? actual = commentsService.GetPostId(commentId);

            // Arrange
            Assert.Equal(expectedPostId, actual);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_Should_Work_Correctly(int commentId)
        {
            // Arrange
            List<Comment> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Comment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICommentsService commentsService = new CommentsService(mockedRepository.Object, null, null);

            string expectedAuthorId = testData.FirstOrDefault(c => c.Id == commentId).AuthorId;
            int? expectedPostId = testData.FirstOrDefault(c => c.Id == commentId).PostId;

            // Act
            CommentViewModel actual = commentsService.GetById<CommentViewModel>(commentId);

            // Arrange
            Assert.Equal(expectedAuthorId, actual.AuthorId);
            Assert.Equal(expectedPostId, actual.PostId);
        }

        [Theory]
        [InlineData("Some content", 1, "1")]
        public async Task CreateAsync_Should_Work_Correctly(string content, int postId, string authorId)
        {
            // Arrange
            List<Comment> testData = this.GetTestData();
            var mockedrepository = new Mock<IDeletableEntityRepository<Comment>>();
            mockedrepository.Setup(x => x.AddAsync(It.IsAny<Comment>()))
                .Callback((Comment comment) =>
                {
                    testData.Add(comment);
                });
            ICommentsService commentsService = new CommentsService(mockedrepository.Object, null, null);
            int expectedCommentsCount = testData.ToList().Count + 1;
            CreateCommentInputModel inputModel = new CreateCommentInputModel()
            {
                Content = content,
                PostId = postId,
                AuthorId = authorId,
            };

            // Act
            await commentsService.CreateAsync(inputModel);

            // Arrange
            Assert.Contains(testData, c => c.Content == content);
            Assert.Equal(expectedCommentsCount, testData.Count);
        }

        [Theory]
        [InlineData(1, "New content")]
        public async Task UpdateAsync_Should_Work_Correctly(int commentId ,string content)
        {
            // Arrange
            List<Comment> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Comment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            mockedRepository.Setup(x => x.Update(It.IsAny<Comment>()))
                .Callback((Comment comment) =>
                {
                    var oldComment = testData.FirstOrDefault(c => c.Id == commentId);
                    oldComment = comment;
                });
            ICommentsService commentsService = new CommentsService(mockedRepository.Object, null, null);
            EditCommentInputModel inputModel = new EditCommentInputModel()
            {
                CommentId = commentId,
                Content = content,
            };

            // Act
            await commentsService.UpdateAsync(inputModel);

            // Arrange
            Assert.True(testData.FirstOrDefault(c => c.Id == commentId).Content == content);
        }

        private List<Comment> GetTestData()
        {
            return new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    PostId = 2,
                    Author = new ApplicationUser()
                    {
                        Id = "2",
                        FirstName = "Josh",
                        LastName = "Noev",
                    },
                    AuthorId = "2",
                    Likes = new List<Like>(),
                    Dislikes = new List<Dislike>(),
                },
                new Comment()
                {
                    Id = 2,
                    PostId = 2,
                    Author = new ApplicationUser()
                    {
                        Id = "3",
                        FirstName = "Niki",
                        LastName = "Peshov",
                    },
                    AuthorId = "3",
                    Likes = new List<Like>(),
                    Dislikes = new List<Dislike>(),
                },
                new Comment()
                {
                    Id = 3,
                    PostId = 1,
                    Author = new ApplicationUser()
                    {
                        Id = "2",
                        FirstName = "Josh",
                        LastName = "Noev",
                    },
                    AuthorId = "2",
                    Likes = new List<Like>(),
                    Dislikes = new List<Dislike>(),
                },
                new Comment()
                {
                    Id = 4,
                    PostId = 1,
                    Author = new ApplicationUser()
                    {
                        Id = "4",
                        FirstName = "Josh",
                        LastName = "Peshov",
                    },
                    AuthorId = "4",
                    Likes = new List<Like>(),
                    Dislikes = new List<Dislike>(),
                },
            };
        }
    }
}
