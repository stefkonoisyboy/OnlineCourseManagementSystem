namespace OnlineCourseManagementSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using OnlineCourseManagementSystem.Web.ViewModels.Shedules;

    public class AllEventsCreatedViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }

        public VideoFileInputModel InputModel { get; set; }

        public CreateSheduleInputModel SheduleInputModel { get; set; }
    }
}
