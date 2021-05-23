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
        Task CreateAsync(CreateAssignmentInputModel inputModel);

        IEnumerable<T> GetAllUsersForAssignment<T>(int assignmentId);

        Task MarkAsSeen(int assignmentId, string userId);

        IEnumerable<T> GetAllBy<T>(int courseId);

        Task<int> DeleteAssignment(int assignmetId);

        IEnumerable<T> GetAllBy<T>(string userId);

        IEnumerable<T> GetAllFinishedBy<T>(string userId);

        T GetById<T>(int assignmentId, string userId);

        Task UpdateAsync(EditAssignmentInputModel inputModel);

        Task<int> TurnIn(FilesToAssignmentInputModel inputModel);

        Task<int> MarkSubmittedAssignment(MarkSubmittedAssignmentInputModel inputModel);

        Task UndoTurnIn(int assignmentId, string userId);

        IEnumerable<T> GetAllCheckedBy<T>(int courseId);

        Task UpdateCheckedAsync(EditCheckedAssignmentInputModel inputModel);

        T GetCheckedBy<T>(int assignmentId, string userId);

        IEnumerable<T> GetAllCheckedUserAssignments<T>(int assignmentId);
    }
}
