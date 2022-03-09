namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllContestsIndexListViewModel
    {
        public IEnumerable<AllActiveContestsViewModel> ActiveContests { get; set; }

        public IEnumerable<AllFinishedContestsViewModel> FinishedContests { get; set; }
    }
}
