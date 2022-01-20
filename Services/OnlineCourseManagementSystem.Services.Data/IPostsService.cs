namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Posts;

    public interface IPostsService
    {
        /// <summary>
        /// This method creates post.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task CreateAsync(CreatePostInputModel inputModel);

        /// <summary>
        /// This method gets all posts.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// This method gets post.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="postId"></param>
        /// <returns></returns>
        T GetById<T>(int postId);

        Task UpdateAsync(EditPostInputModel editPostInputModel);

        Task DeleteAsync(int postId);

        IEnumerable<T> SearchByTitle<T>(string search);

        /// <summary>
        /// This methdo gets all posts by course.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCourseId<T>(int courseId);

        Task Like(int postId, string userId);

        Task Dislike(int postId, string userId);

        /// <summary>
        /// This method gets count of all posts.
        /// </summary>
        /// <returns></returns>
        int GetCoutOfAllPosts();

        /// <summary>
        /// This method gets all posts which are taken by admin.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllByAdmin<T>();
    }
}
