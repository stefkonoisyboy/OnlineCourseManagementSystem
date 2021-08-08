using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Paging
{
    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ActiveCoursesCount / this.ItemsPerPage);

        public int ActiveCoursesCount { get; set; }

        public int ItemsPerPage { get; set; }

        public string Name { get; set; }
    }
}
