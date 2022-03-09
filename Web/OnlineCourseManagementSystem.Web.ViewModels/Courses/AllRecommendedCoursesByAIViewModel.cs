namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class AllRecommendedCoursesByAIViewModel<TI, TO> : PagingViewModel
    {
        public IEnumerable<TI> InputData { get; set; }

        public IEnumerable<TO> OutputData { get; set; }
    }
}
