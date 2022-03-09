namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Paging;

    public class AllLecturesByIdListViewModel : PagingViewModel
    {
        public IEnumerable<AllLecturesByIdViewModel> Lectures { get; set; }
    }
}
