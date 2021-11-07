namespace OnlineCourseManagementSystem.Web.ViewModels.Paging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BasePagingViewModel
    {
        public int PreviousPage => this.PageNumber == 1 ? 1 : this.PageNumber - 1;

        public int NextPage => this.PageNumber == this.PagesCount ? this.PagesCount : this.PageNumber + 1;

        public int PageNumber { get; set; }

        public virtual int ItemsPerPage { get; }

        public int PagesCount { get; set; }
    }
}
