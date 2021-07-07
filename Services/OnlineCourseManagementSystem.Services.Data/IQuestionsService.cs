using OnlineCourseManagementSystem.Web.ViewModels.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IQuestionsService
    {
        Task CreateAsync(CreateQuestionInputModel input);

        Task UpdateAsync(EditQuestionInputModel input);

        Task DeleteAsync(int id);

        int GetCountByExamId(int examId);

        T GetById<T>(int id);

        IEnumerable<T> GetAllByExam<T>(int examId);
    }
}
