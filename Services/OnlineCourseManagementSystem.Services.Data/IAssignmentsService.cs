namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;

    public interface IAssignmentsService
    {
        Task CreateAsync(CreateAssignmentInputModel inputModel);

        IEnumerable<T> GetAllUserForAssignment<T>(int assignmentId);

        Task MarkAsSeen(int assignmentId);

        IEnumerable<T> GetAllBy<T>(int courseId);

        Task<int> DeleteAssignment(int assignmetId);

        IEnumerable<T> GetAllBy<T>(string userId);

        IEnumerable<T> GetAllFinishedBy<T>(string userId);

        T GetById<T>(int id);

        Task UpdateAsync(EditAssignmentInputModel inputModel);
    }
}
