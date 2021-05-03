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
    using OnlineCourseManagementSystem.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;

        public PostsService(IDeletableEntityRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task CreateAsync(CreatePostInputModel inputModel)
        {
            Post post = new Post
            {
                Content = inputModel.Content,
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

        public T GetById<T>(int postId)
        {
            return this.postRepository
                .All()
                .Where(p => p.Id == postId)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> SearchByTitle<T>(SearchInputModel inputModel)
        {
            return this.postRepository
                .All()
                .Where(p => p.Title.Contains(inputModel.Title))
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(EditPostInputModel inputModel)
        {
            Post post = this.postRepository
                .All()
                .FirstOrDefault(p => p.Id == inputModel.PostId);

            post.Content = inputModel.Content;
            post.Title = inputModel.Title;
            post.CourseId = inputModel.CourseId;

            await this.postRepository.SaveChangesAsync();
        }
    }
}
