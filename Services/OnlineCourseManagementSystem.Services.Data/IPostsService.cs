namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task CreateAsync(CreatePostInputModel inputModel);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int postId);

        Task UpdateAsync(EditPostInputModel editPostInputModel);

        Task DeleteAsync(int postId);

        IEnumerable<T> SearchByTitle<T>(string search);

        IEnumerable<T> GetByCourseId<T>(int courseId);

        Task Like(int postId, string userId);

        Task Dislike(int postId, string userId);

        int GetCoutOfAllPosts();

        IEnumerable<T> GetAllByAdmin<T>();
    }
}
