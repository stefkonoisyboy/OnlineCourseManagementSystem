namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data.MachineLearning;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<Like> likesRepository;
        private readonly IDeletableEntityRepository<Dislike> dislikesRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository, IDeletableEntityRepository<Like> likesRepository, IDeletableEntityRepository<Dislike> dislikesRepository)
        {
            this.commentsRepository = commentRepository;
            this.likesRepository = likesRepository;
            this.dislikesRepository = dislikesRepository;
        }

        public async Task CreateAsync(CreateCommentInputModel inputModel)
        {
            Comment comment = new Comment
            {
                Content = inputModel.Content,
                PostId = inputModel.PostId,
                AuthorId = inputModel.AuthorId,
            };

            await this.commentsRepository.AddAsync(comment);

            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task<int?> DeleteAsync(int commentId)
        {
            Comment comment = this.commentsRepository.All().FirstOrDefault(c => c.Id == commentId);
            int? postId = comment.PostId;
            this.commentsRepository.Delete(comment);
            await this.commentsRepository.SaveChangesAsync();
            return postId;
        }

        public IEnumerable<T> GetAllByPostId<T>(int postId)
        {
            ICollection<Comment> commentsToUse = this.commentsRepository
               .All()
               .Where(c => c.PostId == postId && c.ParentId == null)
               .ToArray();

            Dictionary<int, DateTime?> commentDictionary = new Dictionary<int, DateTime?>();

            foreach (var comment in commentsToUse)
            {
                if (comment.ModifiedOn != null)
                {
                    if (comment.ModifiedOn > comment.CreatedOn)
                    {
                        commentDictionary.Add(comment.Id, comment.ModifiedOn);
                    }
                }
                else
                {
                    commentDictionary.Add(comment.Id, comment.CreatedOn);
                }
            }

            ICollection<T> comments = new List<T>();

            foreach (var comment in commentDictionary.OrderByDescending(c => c.Value))
            {
                T commentGet = this.commentsRepository
                    .All()
                    .Where(x => x.Id == comment.Key)
                    .To<T>()
                    .FirstOrDefault();
                comments.Add(commentGet);
            }

            return comments;
        }

        public IEnumerable<T> GetAllReplies<T>(int commentId)
        {
            ICollection<Comment> repliesToComment = this.commentsRepository
               .All()
               .Where(c => c.ParentId != null && c.ParentId == commentId)
               .ToArray();

            Dictionary<int, DateTime?> commentDictionary = new Dictionary<int, DateTime?>();

            foreach (var comment in repliesToComment)
            {
                if (comment.ModifiedOn != null)
                {
                    if (comment.ModifiedOn > comment.CreatedOn)
                    {
                        commentDictionary.Add(comment.Id, comment.ModifiedOn);
                    }
                }
                else
                {
                    commentDictionary.Add(comment.Id, comment.CreatedOn);
                }
            }

            ICollection<T> replies = new List<T>();

            foreach (var comment in commentDictionary.OrderByDescending(c => c.Value))
            {
                T commentGet = this.commentsRepository
                    .All()
                    .Where(x => x.Id == comment.Key)
                    .To<T>()
                    .FirstOrDefault();
                replies.Add(commentGet);
            }

            return replies;
        }

        public T GetById<T>(int commentId)
        {
            return this.commentsRepository.All()
                .Where(c => c.Id == commentId)
                .To<T>()
                .FirstOrDefault();
        }

        public T GetLastActiveCommentByPostId<T>(int postId)
        {
            Comment lastCommentCreatedOn = this.commentsRepository.All().Where(c => c.PostId == postId).OrderByDescending(c => c.CreatedOn).FirstOrDefault();
            Comment lastCommentModifiedOn = this.commentsRepository.All().Where(c => c.PostId == postId).OrderByDescending(c => c.ModifiedOn).FirstOrDefault();

            if (this.commentsRepository.All().Where(c => c.PostId == postId).Any())
            {
                if (lastCommentCreatedOn.CreatedOn > lastCommentModifiedOn.ModifiedOn)
                {
                    return this.commentsRepository
                        .All()
                        .Where(c => c.Id == lastCommentCreatedOn.Id)
                        .To<T>()
                        .FirstOrDefault();
                }
                else
                {
                    return this.commentsRepository
                        .All()
                        .Where(c => c.Id == lastCommentModifiedOn.Id)
                        .To<T>()
                        .FirstOrDefault();
                }
            }
            else
            {
                return default(T);
            }
        }

        public int? GetPostId(int commentId)
        {
            Comment comment = this.commentsRepository
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

            await this.commentsRepository.AddAsync(comment);

            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditCommentInputModel inputModel)
        {
            Comment comment = this.commentsRepository
                .All()
                .FirstOrDefault(c => c.Id == inputModel.CommentId);

            comment.Content = inputModel.Content;

            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task Dislike(int commentId, string userId)
        {
            if (this.dislikesRepository.All().Where(l => l.CreatorId == userId && l.CommentId == commentId).Count() == 0)
            {
                if (this.dislikesRepository.AllWithDeleted().Where(d => d.CommentId == commentId && d.CreatorId == userId).Count() != 0)
                {
                    Dislike dislike = this.dislikesRepository.AllWithDeleted().FirstOrDefault(d => d.CommentId == commentId && d.CreatorId == userId);

                    this.dislikesRepository.Undelete(dislike);
                }
                else
                {
                    Dislike dislike = new Dislike
                    {
                        CommentId = commentId,
                        CreatorId = userId,
                    };

                    await this.dislikesRepository.AddAsync(dislike);
                }
            }
            else
            {
                Dislike dislike = this.dislikesRepository.All().FirstOrDefault(l => l.CreatorId == userId && l.CommentId == commentId);
                this.dislikesRepository.Delete(dislike);
            }

            if (this.likesRepository.All().Where(x => x.CommentId == commentId && x.CreatorId == userId).Count() != 0)
            {
                Like like = this.likesRepository.All().FirstOrDefault(l => l.CreatorId == userId && l.CommentId == commentId);
                this.likesRepository.Delete(like);
            }

            await this.dislikesRepository.SaveChangesAsync();
            await this.likesRepository.SaveChangesAsync();
        }

        public async Task Like(int commentId, string userId)
        {
            if (this.likesRepository.All().Where(x => x.CommentId == commentId && x.CreatorId == userId).Count() == 0)
            {
                if (this.likesRepository.AllWithDeleted().Where(l => l.CommentId == commentId && l.CreatorId == userId).Count() != 0)
                {
                    Like like = this.likesRepository.AllWithDeleted().FirstOrDefault(l => l.CommentId == commentId && l.CreatorId == userId);
                    this.likesRepository.Undelete(like);
                }
                else
                {
                    Like like = new Like
                    {
                        CommentId = commentId,
                        CreatorId = userId,
                    };

                    await this.likesRepository.AddAsync(like);
                }
            }
            else
            {
                Like like = this.likesRepository.All().FirstOrDefault(l => l.CommentId == commentId && l.CreatorId == userId);
                this.likesRepository.Delete(like);
            }

            if (this.dislikesRepository.All().Where(d => d.CommentId == commentId && d.CreatorId == userId).Count() != 0)
            {
                Dislike dislike = this.dislikesRepository.All().FirstOrDefault(l => l.CreatorId == userId && l.CommentId == commentId);
                this.dislikesRepository.Delete(dislike);
            }

            await this.likesRepository.SaveChangesAsync();
            await this.dislikesRepository.SaveChangesAsync();
        }

        public IEnumerable<PredictedCommentViewModel> GetAllCommentsClassified()
        {
            var modelFile = "ToxicCommentsModel.zip";
            var source = this.commentsRepository.All().To<CommentViewModel>().ToArray();
            var predicion = ToxicCommentsClassifier.TestModel(modelFile, source.Select(x => x.Content));

            List<PredictedCommentViewModel> predictedComments = new List<PredictedCommentViewModel>();
            int index = 0;
            foreach (var predictedComment in predicion)
            {
                predictedComments.Add(new PredictedCommentViewModel()
                {
                    Comment = source[index],
                    Prediction = predictedComment.Prediction,
                    Score = predictedComment.Score,
                });

                index++;
            }

            return predictedComments;
        }

        public async Task DeleteAllToxicComments()
        {
            IEnumerable<int> toxicComments = this.GetAllCommentsClassified().Where(c => c.Score > 0.5 && c.Prediction == true).Select(c => c.Comment.Id);
            foreach (var commentId in toxicComments)
            {
                Console.WriteLine(commentId);
                Comment comment = this.commentsRepository.All().FirstOrDefault(c => c.Id == commentId);
                this.commentsRepository.Delete(comment);
            }

            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
