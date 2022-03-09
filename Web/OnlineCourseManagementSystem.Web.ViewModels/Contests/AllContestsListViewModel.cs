namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class AllContestsListViewModel : PagingViewModel
    {
        public IEnumerable<AllContestsViewModel> Contests { get; set; }
    }
}
