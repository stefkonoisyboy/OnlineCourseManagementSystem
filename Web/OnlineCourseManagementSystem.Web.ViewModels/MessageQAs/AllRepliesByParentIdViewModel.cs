using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.MessageQAs
{
    public class AllRepliesByParentIdViewModel : AllMessagesByChannelIdViewModel
    {
        public int ParentId { get; set; }
    }
}
