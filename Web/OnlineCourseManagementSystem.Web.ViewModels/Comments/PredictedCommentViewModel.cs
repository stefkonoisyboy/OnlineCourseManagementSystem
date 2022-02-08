namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PredictedCommentViewModel
    {
        public CommentViewModel Comment { get; set; }

        public bool Prediction { get; set; }

        public float Score { get; set; }
    }
}
