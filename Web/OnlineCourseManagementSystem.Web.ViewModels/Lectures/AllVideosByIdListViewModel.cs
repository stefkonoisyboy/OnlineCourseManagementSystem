using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    public class AllVideosByIdListViewModel
    {
        public IEnumerable<AllVideosByIdViewModel> Videos { get; set; }
    }
}
