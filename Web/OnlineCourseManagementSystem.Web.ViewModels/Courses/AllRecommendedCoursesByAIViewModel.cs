using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    public class AllRecommendedCoursesByAIViewModel<TI, TO> : PagingViewModel
    {
        public IEnumerable<TI> InputData { get; set; }

        public IEnumerable<TO> OutputData { get; set; }
    }
}
