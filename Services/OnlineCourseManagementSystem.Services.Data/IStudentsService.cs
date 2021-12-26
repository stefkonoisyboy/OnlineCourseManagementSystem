namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.ViewModels.Students;

    public interface IStudentsService
    {
        /// <summary>
        /// This method gets student full name by studentId.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        string GetFullNameById(string studentId);

        /// <summary>
        /// This method gets all students.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// This method gets all students by parentId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllById<T>(string parentId);

        /// <summary>
        /// This method gets all students by courseId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCourse<T>(int courseId);

        /// <summary>
        /// Thies method gets all students as selectListItme.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        /// <summary>
        /// This method gets all students by parentId as selectListItems.
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllByParentAsSelectListItems(string parentId);

        /// <summary>
        /// This method adds parent to student.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddParent(AddParentInputModel input);

        /// <summary>
        /// This method gets all students by courseId as selectListItems.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAsSelectListItemsByCourse(int courseId);
    }
}
