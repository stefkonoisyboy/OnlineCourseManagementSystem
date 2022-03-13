namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Ganss.XSS;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Trainings;

    public class TrainingsService : ITrainingsService
    {
        private readonly IDeletableEntityRepository<Training> trainingRepository;
        private readonly CloudinaryService cloudinaryService;

        public TrainingsService(IDeletableEntityRepository<Training> trainingRepository, Cloudinary cloudinaryUtility)
        {
            this.trainingRepository = trainingRepository;
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
        }

        public async Task CreateAsync(CreateTrainingInputModel inputModel)
        {
            Training training = new Training()
            {
                Name = inputModel.Name,
                Description = new HtmlSanitizer().Sanitize(inputModel.Description),
                TrainingType = (TrainingType)Enum.Parse(typeof(TrainingType), inputModel.TrainingType, true),
            };

            if (string.IsNullOrEmpty(inputModel.Link) && inputModel.Image == null)
            {
                throw new ArgumentException("No link or image uploaded!");
            }

            if (string.IsNullOrEmpty(inputModel.Link))
            {
                string fileName = Guid.NewGuid().ToString() + inputModel.Image.Name;
                string extension = System.IO.Path.GetExtension(inputModel.Image.FileName);

                training.ImageRemoteUrl = await this.cloudinaryService.UploadFile(inputModel.Image, fileName, extension);
            }
            else
            {
                training.ImageRemoteUrl = inputModel.Link;
            }

            foreach (var moduleId in inputModel.ModuleIds)
            {
                training.Modules.Add(new TrainingModule() { ModuleId = moduleId });
            }

            await this.trainingRepository.AddAsync(training);
            await this.trainingRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.trainingRepository
                .All()
                .To<T>()
                .ToList();
        }
    }
}
