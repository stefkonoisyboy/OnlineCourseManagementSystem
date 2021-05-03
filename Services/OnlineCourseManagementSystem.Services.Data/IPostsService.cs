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

        IEnumerable<T> SearchByTitle<T>(SearchInputModel inputModel);
    }
}
