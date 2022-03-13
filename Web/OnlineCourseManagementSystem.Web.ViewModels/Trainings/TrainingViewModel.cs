namespace OnlineCourseManagementSystem.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Modules;

    public class TrainingViewModel : IMapFrom<Training>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageRemoteUrl { get; set; }

        public IEnumerable<ModuleViewModel> Modules { get; set; }
    }
}
