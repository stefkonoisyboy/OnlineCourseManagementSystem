namespace OnlineCourseManagementSystem.Web.ViewModels.MessageQAs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllRepliesByParentIdListViewModel
    {
        public IEnumerable<AllRepliesByParentIdViewModel> Replies { get; set; }
    }
}
