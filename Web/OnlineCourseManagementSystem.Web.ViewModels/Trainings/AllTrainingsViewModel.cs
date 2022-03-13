namespace OnlineCourseManagementSystem.Web.ViewModels.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllTrainingsViewModel
    {
        public IEnumerable<TrainingViewModel> Trainings { get; set; }
    }
}
