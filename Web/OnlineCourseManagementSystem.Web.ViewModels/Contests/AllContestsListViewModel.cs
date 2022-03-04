using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    public class AllContestsListViewModel : PagingViewModel
    {
        public IEnumerable<AllContestsViewModel> Contests { get; set; }
    }
}
