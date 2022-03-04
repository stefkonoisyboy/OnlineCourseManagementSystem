using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    public class AllLecturesByIdListViewModel : PagingViewModel
    {
        public IEnumerable<AllLecturesByIdViewModel> Lectures { get; set; }
    }
}
