namespace OnlineCourseManagementSystem.Web.ViewModels.Trainings
{
    using OnlineCourseManagementSystem.Web.ViewModels.Modules;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllTrainingsViewModel
    {
        public IEnumerable<TrainingViewModel> Trainings { get; set; }

        public ModuleViewModel Module { get; set; }

        public ModuleViewModel Fundamentals { get; set; }
    }
}
