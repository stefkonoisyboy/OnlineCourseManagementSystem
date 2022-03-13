namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Web.ViewModels.Trainings;

    public interface ITrainingsService
    {
        Task CreateAsync(CreateTrainingInputModel inputModel);

        IEnumerable<T> GetAll<T>();
    }
}
