namespace OnlineCourseManagementSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class AllEventsViewModel : BasePagingViewModel
    {
        public IEnumerable<EventViewModel> EventsComing { get; set; }

        public IEnumerable<EventViewModel> EventsFinished { get; set; }

        public override int ItemsPerPage => 4;
    }
}
