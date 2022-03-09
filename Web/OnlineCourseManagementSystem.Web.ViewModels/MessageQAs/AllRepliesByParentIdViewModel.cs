namespace OnlineCourseManagementSystem.Web.ViewModels.MessageQAs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllRepliesByParentIdViewModel : AllMessagesByChannelIdViewModel
    {
        public int ParentId { get; set; }
    }
}
