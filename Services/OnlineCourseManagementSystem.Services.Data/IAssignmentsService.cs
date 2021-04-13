namespace OnlineCourseManagementSystem.Services.Data
{
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAssignmentsService
    {
        Task CreateAsync(CreateAssignmentInputModel inputModel);

        T GetBy<T>(int assingmentId);

        Task MarkAsSeen(int assignmentId);

        //TODO:
        //Task UpdateAsync(string lectureId, int courseId, List<string> studentsId);

        IEnumerable<T> GetAllBy<T>(int courseId);

        void DeleteAssignment(int assignmetId);

        IEnumerable<T> GetAllBy<T>(string userId);

        IEnumerable<T> GetAllFinishedBy<T>(string userId);

    }
}
