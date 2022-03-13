namespace OnlineCourseManagementSystem.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class CreateTrainingInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> ModuleItems { get; set; }

        [Required]
        public IEnumerable<int> ModuleIds { get; set; }

        [Required]
        public string TrainingType { get; set; }

        public IEnumerable<string> TrainingTypeItems => this.GetAllTrainingTypes();

        public string Link { get; set; }

        public IFormFile Image { get; set; }

        private IEnumerable<string> GetAllTrainingTypes()
        {
            foreach (var name in Enum.GetNames(typeof(TrainingType)))
            {
                yield return name;
            }
        }
    }
}
