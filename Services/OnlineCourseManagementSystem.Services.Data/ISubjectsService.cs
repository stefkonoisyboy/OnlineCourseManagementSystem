namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.ViewModels.Subjects;

    public interface ISubjectsService
    {
        /// <summary>
        /// This method gets all subjects as list items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAsSelectListItems();

        /// <summary>
        /// This method gets all subjects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// This method gets all subjects that refer to course c#.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllWithReferedCsharpCourses<T>();

        /// <summary>
        /// This method gets all subjects that refer to course Java.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllWithReferedJavaCourses<T>();

        IEnumerable<T> GetAllWithReferedJSCourses<T>();

        /// <summary>
        /// This method gets subject by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T GetByName<T>(string name);

        /// <summary>
        /// This method gets all subjects by id and course name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjectId"></param>
        /// <param name="courseName"></param>
        /// <returns></returns>
        IEnumerable<T> GetByIdAndCourseName<T>(int subjectId, string courseName);

        /// <summary>
        /// This method gets subject by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);

        /// <summary>
        /// Thsi method creates subject.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task CreateAsync(SubjectInputModel inputModel);

        /// <summary>
        /// This method updates subject.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task UpdateAsync(EditSubjectInputModel inputModel);

        IEnumerable<T> GetAllByModule<T>(int moduleId);
    }
}
