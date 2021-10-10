using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.MessageQAs
{
    public class AllRepliesByParentIdListViewModel
    {
        public IEnumerable<AllRepliesByParentIdViewModel> Replies { get; set; }
    }
}
