namespace OnlineCourseManagementSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllEventsViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
