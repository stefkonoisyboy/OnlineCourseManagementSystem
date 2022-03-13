namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.ViewModels.Modules;

    public interface IModulesService
    {
        /// <summary>
        /// Creates module.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task Create(CreateModuleInputModel inputModel);

        /// <summary>
        /// Updates module.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task UpdateAsync(UpdateModuleInputModel inputModel);

        /// <summary>
        /// Gets module.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        T GetById<T>(int moduleId);

        /// <summary>
        /// Gets all modules by training.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllByTraining<T>(int trainingId);

        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}
