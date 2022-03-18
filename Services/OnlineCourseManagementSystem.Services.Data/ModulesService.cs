namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Modules;

    public class ModulesService : IModulesService
    {
        private readonly IDeletableEntityRepository<ModuleEntity> modulesRepository;
        private readonly IDeletableEntityRepository<TrainingModule> trainingModulesRepository;
        private readonly IDeletableEntityRepository<Subject> subjectsRepository;

        public ModulesService(
            IDeletableEntityRepository<ModuleEntity> modulesRepository,
            IDeletableEntityRepository<TrainingModule> trainingModulesRepository,
            IDeletableEntityRepository<Subject> subjectsRepository)
        {
            this.modulesRepository = modulesRepository;
            this.trainingModulesRepository = trainingModulesRepository;
            this.subjectsRepository = subjectsRepository;
        }

        public async Task Create(CreateModuleInputModel inputModel)
        {
            ModuleEntity module = new ModuleEntity()
            {
                Name = inputModel.Name,
                Description = new HtmlSanitizer().Sanitize(inputModel.Description),
            };

            await this.modulesRepository.AddAsync(module);
            await this.modulesRepository.SaveChangesAsync();

            foreach (var subjectId in inputModel.SubjectIds)
            {
                Subject subject = this.subjectsRepository.All().FirstOrDefault(s => s.Id == subjectId);
                subject.ModuleId = module.Id;
                await this.subjectsRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.modulesRepository
                .All()
                .OrderBy(m => m.Name)
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString(),
                })
                .ToList();
        }

        public IEnumerable<T> GetAllByTraining<T>(int trainingId)
        {
            return this.trainingModulesRepository
                .All()
                .Where(m => m.TrainingId == trainingId)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int moduleId)
        {
            return this.modulesRepository
                .All()
                .Where(m => m.Id == moduleId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(UpdateModuleInputModel inputModel)
        {
            ModuleEntity module = this.modulesRepository.All().FirstOrDefault(m => m.Id == inputModel.Id);
            module.Name = inputModel.Name;
            module.Description = inputModel.Description;

            await this.modulesRepository.SaveChangesAsync();
        }
    }
}
