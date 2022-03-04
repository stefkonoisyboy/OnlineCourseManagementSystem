using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    public class AllContestsIndexListViewModel
    {
        public IEnumerable<AllActiveContestsViewModel> ActiveContests { get; set; }

        public IEnumerable<AllFinishedContestsViewModel> FinishedContests { get; set; }
    }
}
