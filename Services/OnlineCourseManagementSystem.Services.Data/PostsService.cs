namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly IDeletableEntityRepository<Dislike> dislikesRepository;
        private readonly IDeletableEntityRepository<Like> likesRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository, IDeletableEntityRepository<Dislike> dislikesRepository, IDeletableEntityRepository<Like> likesRepository)
        {
            this.postRepository = postsRepository;
            this.likesRepository = likesRepository;
            this.dislikesRepository = dislikesRepository;
        }

        public int GetCoutOfAllPosts()
        {
            return this.postRepository.All().Count();
        }

        public async Task CreateAsync(CreatePostInputModel inputModel)
        {
            Post post = new Post
            {
                Content = new HtmlSanitizer().Sanitize(inputModel.Content),
                Title = inputModel.Title,
                CourseId = inputModel.CourseId,
                AuthorId = inputModel.AuthorId,
            };

            await this.postRepository.AddAsync(post);
            await this.postRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int postId)
        {
            Post post = this.postRepository
                .All()
                .FirstOrDefault(p => p.Id == postId);

            this.postRepository.Delete(post);

            await this.postRepository.SaveChangesAsync();
        }

        public async Task Dislike(int postId, string userId)
        {
            if (this.dislikesRepository.All().Where(l => l.CreatorId == userId && l.PostId == postId).Count() == 0)
            {
                if (this.dislikesRepository.AllWithDeleted().Where(d => d.PostId == postId && d.CreatorId == userId).Count() != 0)
                {
                    Dislike dislike = this.dislikesRepository.AllWithDeleted().FirstOrDefault(d => d.PostId == postId && d.CreatorId == userId);

                    this.dislikesRepository.Undelete(dislike);
                }
                else
                {
                    Dislike dislike = new Dislike
                    {
                        PostId = postId,
                        CreatorId = userId,
                    };

                    await this.dislikesRepository.AddAsync(dislike);
                }
            }
            else
            {
                Dislike dislike = this.dislikesRepository.All().FirstOrDefault(l => l.CreatorId == userId && l.PostId == postId);
                this.dislikesRepository.Delete(dislike);
            }

            if (this.likesRepository.All().Where(x => x.PostId == postId && x.CreatorId == userId).Count() != 0)
            {
                Like like = this.likesRepository.All().FirstOrDefault(l => l.CreatorId == userId && l.PostId == postId);
                this.likesRepository.Delete(like);
            }

            await this.dislikesRepository.SaveChangesAsync();
            await this.likesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.postRepository
                .All()
                .OrderByDescending(p => p.Comments.OrderByDescending(c => c.CreatedOn).FirstOrDefault().CreatedOn)
                .ThenByDescending(p => p.Comments.OrderByDescending(c => c.ModifiedOn).FirstOrDefault().ModifiedOn)
                .ThenByDescending(p => p.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByCourseId<T>(int courseId)
        {
            return this.postRepository
                .All()
                .Where(p => p.CourseId == courseId)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int postId)
        {
            return this.postRepository
                .All()
                .Where(p => p.Id == postId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task Like(int postId, string userId)
        {
            if (this.likesRepository.All().Where(x => x.PostId == postId && x.CreatorId == userId).Count() == 0)
            {
                if (this.likesRepository.AllWithDeleted().Where(l => l.PostId == postId && l.CreatorId == userId).Count() != 0)
                {
                    Like like = this.likesRepository.AllWithDeleted().FirstOrDefault(l => l.PostId == postId && l.CreatorId == userId);
                    this.likesRepository.Undelete(like);
                }
                else
                {
                    Like like = new Like
                    {
                        PostId = postId,
                        CreatorId = userId,
                    };

                    await this.likesRepository.AddAsync(like);
                }
            }
            else
            {
                Like like = this.likesRepository.All().FirstOrDefault(l => l.PostId == postId && l.CreatorId == userId);
                this.likesRepository.Delete(like);
            }

            if (this.dislikesRepository.All().Where(x => x.PostId == postId && x.CreatorId == userId).Count() != 0)
            {
                Dislike dislike = this.dislikesRepository.All().FirstOrDefault(l => l.CreatorId == userId && l.PostId == postId);
                this.dislikesRepository.Delete(dislike);
            }

            await this.likesRepository.SaveChangesAsync();
            await this.dislikesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> SearchByTitle<T>(string search)
        {
            return this.postRepository
                .All()
                .Where(p => p.Title.Contains(search))
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(EditPostInputModel inputModel)
        {
            Post post = this.postRepository
                .All()
                .FirstOrDefault(p => p.Id == inputModel.Id);

            post.Content = new HtmlSanitizer().Sanitize(inputModel.Content);
            post.Title = inputModel.Title;
            post.CourseId = inputModel.CourseId;

            await this.postRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByAdmin<T>()
        {
            return this.postRepository
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .To<T>()
                .ToList();
        }
    }
}
