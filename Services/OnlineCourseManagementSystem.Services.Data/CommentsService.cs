namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task CreateAsync(CreateCommentInputModel inputModel)
        {
            Comment comment = new Comment
            {
                Content = inputModel.Content,
                PostId = inputModel.PostId,
                AuthorId = inputModel.AuthorId,
            };

            await this.commentRepository.AddAsync(comment);

            await this.commentRepository.SaveChangesAsync();
        }

        public async Task<int?> DeleteAsync(int commentId)
        {
            Comment comment = this.commentRepository.All().FirstOrDefault(c => c.Id == commentId);
            int? postId = comment.PostId;
            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
            return postId;
        }

        public IEnumerable<T> GetAllByPostId<T>(int postId)
        {
            return this.commentRepository
                .All()
                .Where(c => c.PostId == postId && c.ParentId == null)
                .OrderBy(c => c.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllReplies<T>(int commentId)
        {
            return this.commentRepository
                .All()
                .Where(c => c.ParentId != null && c.ParentId == commentId)
                .OrderBy(c => c.CreatedOn)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int commentId)
        {
            return this.commentRepository.All()
                .Where(c => c.Id == commentId)
                .To<T>()
                .FirstOrDefault();
        }

        public T GetLastActiveCommentByPostId<T>(int postId)
        {
            return this.commentRepository
                .All()
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .FirstOrDefault();
        }

        public int? GetPostId(int commentId)
        {
            Comment comment = this.commentRepository
                .All()
                .FirstOrDefault(c => c.Id == commentId);

            int? postId = comment.PostId;

            return postId;
        }

        public async Task ReplyToComment(ReplyToCommentInputModel inputModel)
        {
            Comment comment = new Comment
            {
                Content = inputModel.Content,
                PostId = inputModel.PostId,
                AuthorId = inputModel.AuthorId,
                ParentId = inputModel.ParentId,
            };

            await this.commentRepository.AddAsync(comment);

            await this.commentRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditCommentInputModel inputModel)
        {
            Comment comment = this.commentRepository
                .All()
                .FirstOrDefault(c => c.Id == inputModel.CommentId);

            comment.Content = inputModel.Content;

            await this.commentRepository.SaveChangesAsync();
        }
    }
}
