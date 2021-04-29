namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.Absences;

    public interface IAbsencesService
    {
        Task CreateAsync(CreateAbsenceListInputModel input);

        Task UpdateAsync(EditAbsenceInputModel input);

        Task UpdateMultipleAsync(IEnumerable<EditAbsenceInputModel> input);

        Task DeleteAsyncForSpecificLecture(int courseId, int lectureId);

        Task DeleteAsyncForSpecificStudent(int courseId, int lectureId, string studentId);

        IEnumerable<T> GetAllByStudentInSpecifiedRange<T>(GetAllByStudentInIntervalInputModel input);

        IEnumerable<T> GetAllByCourseAndLecture<T>(int courseId, int lectureId);

        T GetById<T>(int id);
    }
}
