namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public interface IUsersService
    {

        /// <summary>
        /// This method gets all users.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// This method gets top 3 users by courseId ordered by the count of assignments for course
        /// they have in descending way.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IEnumerable<T> GetTop3ByCourseId<T>(int courseId);

        /// <summary>
        /// This method gets top 4 teachers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetTop4Teachers<T>();

        /// <summary>
        /// This method gets top 4 students ordered by their average grade of all exams in descending way.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetTop4Students<T>();

        /// <summary>
        /// This method gets user information by userId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(string id);

        /// <summary>
        /// This method gets the fullName of user by userId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetFullNameById(string id);

        /// <summary>
        /// This method gets the profilePicture of user by userId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetProfileImageUrlById(string id);

        /// <summary>
        /// This method updates the profile of user.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task UpdateAsync(ManageAccountInputModel inputModel);

        /// <summary>
        /// This method deletes user by userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteAsync(string userId);

        /// <summary>
        /// This method gets all students ordered by firstName then by lastName.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllStudents<T>();

        /// <summary>
        /// This method gets all lecturers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllLecturers<T>();

        // T GetCourseCreatorById<T>(int courseId);
    }
}
