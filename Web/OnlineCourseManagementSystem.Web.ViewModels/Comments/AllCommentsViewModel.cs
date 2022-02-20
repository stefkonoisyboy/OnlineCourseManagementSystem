namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AllCommentsViewModel
    {
        public IEnumerable<int> DeletedToxicCommentIds { get; set; }

        public IEnumerable<PredictedCommentViewModel> Comments { get; set; }
    }
}
