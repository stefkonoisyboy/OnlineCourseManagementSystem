namespace OnlineCourseManagementSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllEventsCreatedViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
