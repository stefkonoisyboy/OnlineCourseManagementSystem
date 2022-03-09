namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public interface IAssignmentsService
    {
        /// <summary>
        /// This method creates assignment.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task CreateAsync(CreateAssignmentInputModel inputModel);

        /// <summary>
        /// This method gets all users for assignment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllUsersForAssignment<T>(int assignmentId);

        /// <summary>
        /// Thes method marks assignment as seen from user.
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task MarkAsSeen(int assignmentId, string userId);

        /// <summary>
        /// This method gets all assignemnts by coutrse.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllBy<T>(int courseId);

        /// <summary>
        /// This method gets all assignments by course and user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCourseAndUser<T>(int courseId, string userId);

        /// <summary>
        /// This method gets all finished(checked from lecture and turned from user) assignments by course and user. .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllFinishedByCourseAndUser<T>(int courseId, string userId);

        /// <summary>
        /// This method deletes assignment.
        /// </summary>
        /// <param name="assignmetId"></param>
        /// <returns></returns>
        Task<int> DeleteAssignment(int assignmetId);

        /// <summary>
        /// This method gets all assignments by user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllBy<T>(string userId);

        /// <summary>
        /// This method gets all assignments by user that are finished(checked by lecturer).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllFinishedBy<T>(string userId);

        /// <summary>
        /// This method gets assignment by assignmentId and userId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assignmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        T GetById<T>(int assignmentId, string userId);

        /// <summary>
        /// This method gets assignment by assignmentId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        T GetById<T>(int assignmentId);

        /// <summary>
        /// This method updates assignment.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task UpdateAsync(EditAssignmentInputModel inputModel);

        /// <summary>
        /// This method turns in assignment.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task<int> TurnIn(FilesToAssignmentInputModel inputModel);

        /// <summary>
        /// This method marks submitted assignment for user.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task<int> MarkSubmittedAssignment(MarkSubmittedAssignmentInputModel inputModel);

        /// <summary>
        /// This method undo turns in assignment.
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task UndoTurnIn(int assignmentId, string userId);

        /// <summary>
        /// This method gets all assignments by course that are checked.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllCheckedBy<T>(int courseId);

        /// <summary>
        /// This method updates checked assignment for user.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task UpdateCheckedAsync(EditCheckedAssignmentInputModel inputModel);

        /// <summary>
        /// This method gets assignment that is checked by assignmentId and userId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assignmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        T GetCheckedBy<T>(int assignmentId, string userId);

        /// <summary>
        /// This method gets all users for assignment that are checked.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllCheckedUserAssignments<T>(int assignmentId);

        /// <summary>
        /// This method gets all assignments by admin.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllByAdmin<T>();

        /// <summary>
        /// This method gets all assignments created by current lecture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllByLecturer<T>(string lecturerId);
    }
}
